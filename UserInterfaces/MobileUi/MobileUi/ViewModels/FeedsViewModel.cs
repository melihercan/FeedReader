using Domain.Entities;
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
            await Shell.Current.GoToAsync($"/feedchannel?feedchanneljson={feedChannelJson}");
        });
    }
}
