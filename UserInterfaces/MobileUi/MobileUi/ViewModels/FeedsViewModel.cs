﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application.UseCases;
using Xamarin.Forms;
using System.Text.Json;
using Ardalis.Result;
using Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Windows.Input;
using System.Web;

namespace MobileUi.ViewModels
{
    public class FeedsViewModel : BaseViewModel
   {
        public ObservableCollection<FeedChannel> FeedChannels { get; set; }

        private readonly ILogger<FeedsViewModel> _logger;
        private readonly IMediator _mediator;

        public FeedsViewModel()
        {
            _logger = Registry.ServiceProvider.GetService <ILogger<FeedsViewModel>>();
            _mediator = Registry.ServiceProvider.GetService<IMediator>();
            FeedChannels = new ObservableCollection<FeedChannel>();
        }


        internal override async Task OnViewAppearingAsync()
        {
            await GetFeedChannelsAsync();
        }

        private async Task GetFeedChannelsAsync()
        {
            var resultGetAllFeeds = await _mediator.Send(new GetAllFeeds { });
            if (resultGetAllFeeds.Status != ResultStatus.Ok)
            {
                _logger.LogError(string.Join(",", resultGetAllFeeds.Errors));
                return;
            }
            FeedChannels.Clear();
            foreach (var feedChannel in resultGetAllFeeds.Value)
            {
                FeedChannels.Add(feedChannel);
            }
        }

        public ICommand FeedChannelSelectedCommand => new Command<FeedChannel>(async feedChannel =>
        {
            var feedChannelJson = JsonSerializer.Serialize(feedChannel);
            // Bug https://github.com/xamarin/Xamarin.Forms/issues/10899 strips the first slash.
            // "https://" --> "https:/"
            // For now, encode the json first.
            feedChannelJson = HttpUtility.UrlEncode(feedChannelJson);


            await Shell.Current.GoToAsync($"feedchannel?feedchanneljson={feedChannelJson}");
        });

        public ICommand AddFeedChannelCommand => new Command(async () => 
        {
            //// TODO: Anti pattern (UI in view model) ---> create popup service...
            string result = await App.Current.MainPage.DisplayPromptAsync(string.Empty, "Enter feed URL");
            _logger.LogInformation($"result:{result}");
            if (string.IsNullOrEmpty(result))
            {
                return;
            }

            var url = result;
            var feedResult = await _mediator.Send(new AddFeed
            {
                Url = url
            });
            if (feedResult.Status == ResultStatus.Ok)
            {
                FeedChannels.Add(feedResult.Value);
            }
            else
            {
                _logger.LogError(string.Join(",", feedResult.Errors));
            }
        });

    }
}
