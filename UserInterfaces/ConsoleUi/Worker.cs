using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleUi
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;

        public Worker(ILogger<Worker> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var resultFeedChannel = await _mediator.Send(new AddFeed
            {
                Url = "https://www.cnbc.com/id/100003114/device/rss/rss.html"
            });
            if(resultFeedChannel.Success)
            {
                ShowFeedChannel(resultFeedChannel.Value);
            }
            else
            {
                _logger.LogError(resultFeedChannel.Error);
            }

            var resultFeedChannels = await _mediator.Send(new GetAllFeeds { });
            if (resultFeedChannels.Success)
            {
                foreach (var feedChannel in resultFeedChannels.Value)
                {
                    ShowFeedChannel(feedChannel);
                }
            }
            else
            {
                _logger.LogError(resultFeedChannels.Error);
            }



            //while (!stoppingToken.IsCancellationRequested)
            //{
            //  _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //await Task.Delay(1000, stoppingToken);
            //}
        }

        private void ShowFeedChannel(FeedChannel feedChannel)
        {
            var msg = string.Empty;
            msg += Environment.NewLine;
            msg += $"======== FEED CHANNEL ========{Environment.NewLine}";
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
            _logger.LogInformation(msg);
        }
    }
}
