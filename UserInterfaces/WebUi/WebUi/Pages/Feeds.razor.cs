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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Blazored.Modal.Services;
using WebUi.Models;
using Ardalis.Result;

namespace WebUi.Pages
{
    public partial class Feeds : IDisposable
    {
        private List<FeedChannel> _feedChannels;
        private FeedChannel _selectedFeedChannel;
        private FeedItem _selectedFeedItem;
        private IDisposable _obFeedChannel;

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private ILogger<Feeds> _logger { get; set; }

        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private IModalService _modal { get; set; }

        protected override async Task OnInitializedAsync()
        {

            //// TESTING
            ///
            //var urls = new string[]
            //{
            //    "https://www.nasa.gov/rss/dyn/breaking_news.rss",
            //    "http://feeds.bbci.co.uk/news/world/rss.xml",
            //    //"https://feeds.fireside.fm/xamarinpodcast/rss",
            //    //"https://www.cnbc.com/id/100003114/device/rss/rss.html"
            //};
            //foreach (var url in urls)
            //{
            //    var resultFeedChannel = await _mediator.Send(new Application.UseCases.AddFeed
            //    {
            //        Url = url
            //    });
            //    if (resultFeedChannel.Success)
            //    {
            //        //ShowFeedChannel(resultFeedChannel.Value);
            //    }
            //    else
            //    {
            //        _logger.LogError(resultFeedChannel.Error);
            //    }
            //}
            //// END TESTING

            var resultGet = await _mediator.Send(new GetAllFeeds { });
            if (resultGet.Status == ResultStatus.Ok)
            {
                _feedChannels = resultGet.Value.ToList();
                Console.WriteLine($"---- feedChannels: {_feedChannels.Count()}");
                ////                foreach (var feedChannel in _feedChannels)
                ////            {
                ////            ShowFeedChannel(feedChannel);
                ////    }
                ///
                var resultNotify = await _mediator.Send(new NotifyFeedUpdate { });
                if (resultNotify.Status == ResultStatus.Ok)
                {
                    _obFeedChannel = resultNotify.Value.Subscribe(feedChannel => 
                    {
                        ChannelUpdated(feedChannel);
                    });
                }
                else
                {
                    _logger.LogError(string.Join(",", resultGet.Errors));
                }
            }
            else
            {
                _logger.LogError(string.Join(",", resultGet.Errors));
            }

            await base.OnInitializedAsync();
        }

        public void Dispose()
        {
            _obFeedChannel.Dispose();
        }

        private void ShowFeedChannel(FeedChannel feedChannel)
        {
            var msg = string.Empty;
            msg += Environment.NewLine;
            msg += $"======== FEED CHANNEL ========{Environment.NewLine}";
            msg += $"Id: {feedChannel.FeedChannelId}{Environment.NewLine}";
            msg += $"Title: {feedChannel.Title}{Environment.NewLine}";
            msg += $"Description: {feedChannel.Description}{Environment.NewLine}";
            msg += $"Link: {feedChannel.Link}{Environment.NewLine}";
            msg += $"ImageUrl: {feedChannel.ImageUrl}{Environment.NewLine}";
            foreach (var feedItem in feedChannel.FeedItems)
            {
                msg += $"-------- FEED ITEM --------{Environment.NewLine}";
                msg += $"Id: {feedChannel.FeedChannelId}{Environment.NewLine}";
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

        private async void ItemSelected(MouseEventArgs e, FeedItem feedItem)
        {
            Console.WriteLine($"Item {feedItem.Title} selected");
            _selectedFeedItem = feedItem;

            await NavigateToUrlAsync(_selectedFeedItem.Link);
        }

        private async void AddNewChannel()
        {
            var modal = _modal.Show<AddFeed>("Add new feed");
            var modalResult = await modal.Result;

            if (!modalResult.Cancelled)
            {
                var url = ((FeedUrl)modalResult.Data).Url;

                var feedResult = await _mediator.Send(new Application.UseCases.AddFeed
                {
                    Url = url
                });
                if (feedResult.Status == ResultStatus.Ok)
                {
                    ShowFeedChannel(feedResult.Value);
                    _feedChannels.Add(feedResult.Value);
                    StateHasChanged();
                }
                else
                {
                    _logger.LogError(string.Join(",", feedResult.Errors));
                }
            }
        }

        private async void RemoveChannel()
        {
            var modal = _modal.Show<RemoveFeed>("Remove the feed?");
            var modalResult = await modal.Result;

            if (!modalResult.Cancelled)
            {
                var feedResult = await _mediator.Send(new Application.UseCases.RemoveFeed
                {
                    Id = _selectedFeedChannel.FeedChannelId
                });
                if (feedResult.Status == ResultStatus.Ok)
                {
                    _feedChannels.Remove(_selectedFeedChannel);
                    _selectedFeedChannel = null;
                    StateHasChanged();
                }
                else
                {
                    _logger.LogError(string.Join(",", feedResult.Errors));
                }
            }

        }


        private async void UpdateChannel()
        {
            var feedResult = await _mediator.Send(new Application.UseCases.UpdateFeed
            {
                FeedChannel = _selectedFeedChannel
            });
            if (feedResult.Status == ResultStatus.Ok)
            {
                ChannelUpdated(feedResult.Value);
            }
            else
            {
                _logger.LogError(string.Join(",", feedResult.Errors));
            }
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

        private void ChannelUpdated(FeedChannel feedChannel)
        {
            Console.WriteLine($"##############----- Channel update");

            var index = _feedChannels.FindIndex(fc => fc.FeedChannelId == feedChannel.FeedChannelId);
            _feedChannels[index] = feedChannel;
            if(feedChannel.FeedChannelId == _selectedFeedChannel.FeedChannelId)
            {
                _selectedFeedChannel = feedChannel;
                StateHasChanged();
            }
        }
    }
}
