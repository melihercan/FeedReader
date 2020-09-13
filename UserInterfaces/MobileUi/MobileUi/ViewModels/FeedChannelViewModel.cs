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

namespace MobileUi.ViewModels
{
    [QueryProperty("FeedChannelJson", "feedchanneljson")]
    public class FeedChannelViewModel : BaseViewModel
    {
        public ObservableCollection<FeedItem> FeedItems { get; set; }

        private readonly ILogger<FeedsViewModel> _logger;

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
            FeedItems = new ObservableCollection<FeedItem>();
        }

        internal override async Task OnViewAppearingAsync()
        {
            if (FeedChannel != null)
            {
                FeedItems.Clear();
                foreach (var feedItem in FeedChannel.FeedItems)
                {
                    FeedItems.Add(feedItem);
                }
            }
        }

        public ICommand FeedItemSelectedCommand => new Command<FeedItem>(async feedItem =>
        {
            var uri = new Uri(feedItem.Link);
            await Browser.OpenAsync(uri);
        });
    }
}
