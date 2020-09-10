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

namespace MobileUi.ViewModels
{
    [QueryProperty("FeedChannelJson", "feedchanneljson")]
    public class FeedChannelViewModel : BaseViewModel
    {
        public ObservableCollection<FeedItem> FeedItems { get; set; }

        private readonly ILogger<FeedsViewModel> _logger;
        private readonly IMediator _mediator;

        private FeedChannel _feedChannel;

        private string _feedChannelJson;
        public string FeedChannelJson
        {
            get => _feedChannelJson;
            set
            {
                // Bug https://github.com/xamarin/Xamarin.Forms/issues/10899 strips the first slash.
                // "https://" --> "https:/"
                // "value" is Base64 encoded.
                var feedChannelJson = Uri.UnescapeDataString(Encoding.UTF8.GetString(Convert.FromBase64String(value)));
                _feedChannel = JsonSerializer.Deserialize<FeedChannel>(feedChannelJson);
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
            FeedItems.Clear();
            foreach (var feedItem in _feedChannel.FeedItems)
            {
                FeedItems.Add(feedItem);
            }
        }


        public ICommand FeedItemSelectedCommand => new Command<FeedItem>(async feedItem =>
        {
            var uri = new Uri(feedItem.Link);
            await Browser.OpenAsync(uri);
            //var feedItemJson = JsonSerializer.Serialize(feedItem);
            //await Shell.Current.GoToAsync($"/feeditem?feeditemjson={feedItemJson}");
        });

    }
}
