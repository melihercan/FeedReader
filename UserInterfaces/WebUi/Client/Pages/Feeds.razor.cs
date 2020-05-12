using Application.UseCases;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored;
using Blazored.Modal;
using Blazored.Modal.Services;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;

namespace WebUi.Client.Pages
{
    public partial class Feeds
    {
        private FeedChannel[] _feedChannels;
        private string _item = "string.Empty";
 
        [Inject]
        private ILogger<Feeds> _logger { get; set; }

        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private IModalService _modalService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //// TESTING
            ///
            var urls = new string[]
            {
                "http://feeds.bbci.co.uk/news/world/rss.xml",
                "https://www.nasa.gov/rss/dyn/breaking_news.rss",
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
                _modalService.Show<Feeds>(resultFeedChannels.Error);
            }

            await base.OnInitializedAsync();
        }

        private void ShowFeedChannel(FeedChannel feedChannel)
        {
            var msg = string.Empty;
            msg += Environment.NewLine;
            msg += $"======== FEED CHANNELX ========{Environment.NewLine}";
            msg += $"Title: {feedChannel.Title}{Environment.NewLine}";
            msg += $"Description: {feedChannel.Description}{Environment.NewLine}";
            msg += $"Link: {feedChannel.Link}{Environment.NewLine}";
            foreach (var feedItem in feedChannel.FeedItems)
            {
                msg += $"-------- FEED ITEM --------{Environment.NewLine}";
                msg += $"Title: {feedItem.Title}{Environment.NewLine}";
                msg += $"Description: {feedItem.Description}{Environment.NewLine}";
                msg += $"Link: {feedItem.Link}{Environment.NewLine}";
            }
            //_logger.LogInformation(msg);
            Console.WriteLine(msg);
        }

        private void ItemSelected(MouseEventArgs e, FeedItem feedItem)
        {
            Console.WriteLine($"Item {feedItem.Title} clicked");
            _item = feedItem.Description;
        }

    }
}
