using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.Json;
using Domain.Entities;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MediatR;
using Shared;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials;
using System.Web;
using Ardalis.Result;
using Application.UseCases;
using System.Linq;

namespace MobileUi.ViewModels
{
    [QueryProperty("FeedChannelJson", "feedchanneljson")]
    public class FeedChannelViewModel : BaseViewModel, IDisposable
    {
        public ObservableCollection<FeedItem> FeedItems { get; set; }

        private readonly ILogger<FeedsViewModel> _logger;
        private readonly IMediator _mediator;
        private IDisposable _obFeedChannel;

        private FeedChannel _feedChannel;
        public FeedChannel FeedChannel
        {
            get => _feedChannel;
            set => SetProperty(ref _feedChannel, value);
        }

        public string FeedChannelJson
        {
            set
            {
                // Bug https://github.com/xamarin/Xamarin.Forms/issues/10899 strips the first slash.
                // "https://" --> "https:/"
                // "value" is URL encoded.
                var feedChannelJson = Uri.UnescapeDataString(HttpUtility.UrlDecode(value));
                FeedChannel = JsonSerializer.Deserialize<FeedChannel>(feedChannelJson);
                Title = FeedChannel.Title;
            }
        }

        public FeedChannelViewModel()
        {
            _logger = Registry.ServiceProvider.GetService<ILogger<FeedsViewModel>>();
            _mediator = Registry.ServiceProvider.GetService<IMediator>();
            FeedItems = new ObservableCollection<FeedItem>();
        }

        internal override async Task OnViewAppearingAsync()
        {
            RefreshUi();

            var notifyFeedUpdateResult = await _mediator.Send(new NotifyFeedUpdate());
            if (notifyFeedUpdateResult.Status != ResultStatus.Ok)
            {
                _logger.LogError(string.Join(",", notifyFeedUpdateResult.Errors));
                return;
            }

            _obFeedChannel = notifyFeedUpdateResult.Value.Subscribe(feedChannel =>
            {
                ChannelUpdated(feedChannel);
            });


        }

        private void RefreshUi()
        {
            FeedItems.Clear();

            var sortedItems = FeedChannel.FeedItems.OrderByDescending(feedItem => feedItem.PublishDate);
            foreach (var feedItem in sortedItems)
            {
                FeedItems.Add(feedItem);
            }
        }

        public ICommand FeedItemSelectedCommand => new Command<FeedItem>(async feedItem =>
        {
            var uri = new Uri(feedItem.Link);
            await Browser.OpenAsync(uri);
        });

        public ICommand GoBackCommand => new Command(async () => 
        {
            await Shell.Current.GoToAsync("..");
        });

        public ICommand RemoveFeedChannelCommand => new Command(async () =>
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                string.Empty, "Are you sure to remove the feed?","Yes","No");
            if(!answer)
            {
                return;
            }

            var feedResult = await _mediator.Send(new RemoveFeed
            {
                Id = FeedChannel.FeedChannelId
            });
            if (feedResult.Status == ResultStatus.Ok)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                _logger.LogError(string.Join(",", feedResult.Errors));
            }
        });

        public ICommand UpdateFeedChannelCommand => new Command(async () =>
        {
            var feedResult = await _mediator.Send(new UpdateFeed
            {
                FeedChannel = FeedChannel
            });
            if (feedResult.Status == ResultStatus.Ok)
            {
                ChannelUpdated(feedResult.Value);
            }
            else
            {
                _logger.LogError(string.Join(",", feedResult.Errors));
            }

        });

        private void ChannelUpdated(FeedChannel feedChannel)
        {
            Console.WriteLine($"##############----- Channel update");
            FeedChannel = feedChannel;
            RefreshUi();

        }


        public void Dispose()
        {
            _obFeedChannel.Dispose();
        }

    }
}
