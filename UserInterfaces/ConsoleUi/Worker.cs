using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases;
using Ardalis.Result;
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
            var resultGetToken = await _mediator.Send(new GetToken { });
            if (resultGetToken.Status != ResultStatus.Ok)
            {
                _logger.LogError(string.Join(",", resultGetToken.Errors));
                return;
            }


            var resultGetAllFeeds = await _mediator.Send(new GetAllFeeds { });
            if (resultGetAllFeeds.Status != ResultStatus.Ok)
            {
                _logger.LogError(string.Join(",", resultGetAllFeeds.Errors));
                return;
            }

            var feedChannels = resultGetAllFeeds.Value.ToList();
            Console.WriteLine($"---- feedChannels: {feedChannels.Count()}");

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
