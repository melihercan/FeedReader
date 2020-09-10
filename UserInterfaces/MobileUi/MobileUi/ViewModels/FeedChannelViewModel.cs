using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.Json;
using Domain.Entities;

namespace MobileUi.ViewModels
{
    [QueryProperty("FeedChannelJson", "feedchanneljson")]
    public class FeedChannelViewModel : BaseViewModel
    {


        private string _feedChannelJson;
        public string FeedChannelJson
        {
            get => _feedChannelJson;
            set
            {
                var feedChannelJson = Uri.UnescapeDataString(value);
                var feedChannel = JsonSerializer.Deserialize<FeedChannel>(feedChannelJson);

            }
        }
    }
}
