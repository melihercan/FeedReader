using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.Json;

namespace MobileUi.ViewModels
{
    [QueryProperty("FeedItemJson", "feeditemjson")]
    public class FeedItemViewModel : BaseViewModel
    {
        private FeedItem _feedItem;

        private string _feedItemJson;
        public string FeedItemJson
        {
            get => _feedItemJson;
            set
            {
                var feedItemJson = Uri.UnescapeDataString(value);
                _feedItem = JsonSerializer.Deserialize<FeedItem>(feedItemJson);
            }
        }
    }
}
