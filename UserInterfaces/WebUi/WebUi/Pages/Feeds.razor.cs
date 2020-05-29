using Application.UseCases;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace WebUi.Pages
{
    public partial class Feeds : ComponentBase
    {


        private FeedChannel[] _feedChannels;
        private FeedChannel _selectedFeedChannel;
        private FeedItem _selectedFeedItem;

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private ILogger<Feeds> _logger { get; set; }

        [Inject]
        private IMediator _mediator { get; set; }


        protected override async Task OnInitializedAsync()
        {

            //// TESTING
            ///
            var urls = new string[]
            {
                "https://www.nasa.gov/rss/dyn/breaking_news.rss",
                "http://feeds.bbci.co.uk/news/world/rss.xml",
                "https://feeds.fireside.fm/xamarinpodcast/rss",
                "https://www.cnbc.com/id/100003114/device/rss/rss.html"
            };
            foreach (var url in urls)
            {
                var resultFeedChannel = await _mediator.Send(new AddFeed
                {
                    Url = url
                });
                if (resultFeedChannel.Success)
                {
                    //ShowFeedChannel(resultFeedChannel.Value);
                }
                else
                {
                    _logger.LogError(resultFeedChannel.Error);
                }
            }
            //// END TESTING

            var resultFeedChannels = await _mediator.Send(new GetAllFeeds { });
            if (resultFeedChannels.Success)
            {
                _feedChannels = resultFeedChannels.Value.ToArray();
                Console.WriteLine($"---- feedChannels: {_feedChannels.Count()}, feedItems:{_feedChannels.FirstOrDefault().FeedItems.Count()}");
                foreach( var feedChannel in _feedChannels)
                {
                    ShowFeedChannel(feedChannel);
                }
            }
            else
            {
                _logger.LogError(resultFeedChannels.Error);
            }

            await base.OnInitializedAsync();
        }

        private void ShowFeedChannel(FeedChannel feedChannel)
        {
            var msg = string.Empty;
            msg += Environment.NewLine;
            msg += $"======== FEED CHANNEL ========{Environment.NewLine}";
            msg += $"Id: {feedChannel.Id}{Environment.NewLine}";
            msg += $"Title: {feedChannel.Title}{Environment.NewLine}";
            msg += $"Description: {feedChannel.Description}{Environment.NewLine}";
            msg += $"Link: {feedChannel.Link}{Environment.NewLine}";
            msg += $"ImageUrl: {feedChannel.ImageUrl}{Environment.NewLine}";
            foreach (var feedItem in feedChannel.FeedItems)
            {
                msg += $"-------- FEED ITEM --------{Environment.NewLine}";
                msg += $"Id: {feedChannel.Id}{Environment.NewLine}";
                msg += $"Title: {feedItem.Title}{Environment.NewLine}";
                msg += $"Description: {feedItem.Description}{Environment.NewLine}";
                msg += $"Link: {feedItem.Link}{Environment.NewLine}";
                msg += $"PublishDate: {feedItem.PublishDate}{Environment.NewLine}";
                msg += $"ImageUrl: {feedItem.ImageUrl}{Environment.NewLine}";
            }
            //_logger.LogInformation(msg);
            Console.WriteLine(msg);
        }

        private void ChannelSelected(MouseEventArgs e, FeedChannel feedChannel)
        {
            Console.WriteLine($"Channel {feedChannel.Title} selected");
            _selectedFeedChannel = feedChannel;
        }

        private async Task ItemSelected(MouseEventArgs e, FeedItem feedItem)
        {
            Console.WriteLine($"Item {feedItem.Title} selected");
            _selectedFeedItem = feedItem;

            await NavigateToUrlAsync(_selectedFeedItem.Link);
        }

        private async Task NavigateToUrlAsync(string url, bool openInNewTab = true)
        {
            if (openInNewTab)
            {
                await _jsRuntime.InvokeAsync<object>("open", url, "_blank");
            }
            else
            {
                _navigationManager.NavigateTo(url);
            }
        }

    }
}
