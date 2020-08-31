using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
            var tokenResult = await _mediator.Send(new GetToken { });
            if (tokenResult.Status == ResultStatus.Ok)
            {
                if (tokenResult.Value == null) //// TODO: or expired
                {
////                    var schemesResult = await _mediator.Send(new GetAuthenticationSchemes { });
    ////                if (schemesResult.Status == ResultStatus.Ok)
        ////            {
            ////            var schemesJson = JsonSerializer.Serialize(schemesResult.Value);
                ////    }
                }
                else
                {
                }
            }

            var loginResult = await _mediator.Send(new Login
            {
                User = new User
                {
                    Username = "melihercan@gmail.com",
                    Password = "Fenerbahce1907#",
                    RememberMe = false
                }
            });
            if (loginResult.Status == ResultStatus.Ok)
            {
                var token = loginResult.Value;
            }



            if (tokenResult.Status != ResultStatus.Ok)
            {
                _logger.LogError(string.Join(",", tokenResult.Errors));
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
