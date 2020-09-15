using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Application.UseCases;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            var tokenResult = await _mediator.Send(new GetToken());
            if (tokenResult.Status != ResultStatus.Ok)
            {
                _logger.LogError(string.Join(",", tokenResult.Errors));
                return;
            }

            if (tokenResult.Value == null) //// TODO: or expired
            {
                var schemesResult = await _mediator.Send(new GetAuthenticationSchemes());
                if (schemesResult.Status == ResultStatus.Ok)
                {
                    var schemes = schemesResult.Value;
                    await ExecuteLoginAsync(schemes);
                }
            }

            await ExecuteFeedsAsync();



            //while (!stoppingToken.IsCancellationRequested)
            //{
            //  _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //await Task.Delay(1000, stoppingToken);
            //}

            ////var text = HttpUtility.HtmlDecode(html);
        }

        private async Task ExecuteLoginAsync(string[] schemes)
        {
            var selection = GetSchemeSelection();
            if(selection == 1)
            {
                Console.Write("\nUsername: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                Console.WriteLine("Logging in...\n");
                var loginResult = await _mediator.Send(new Login
                {
                    User = new User
                    {
                        Username = username,
                        Password = password,
                        RememberMe = false
                    }
                });
                if (loginResult.Status == ResultStatus.Ok)
                {
                    var token = loginResult.Value;
                }
            }
            else
            {
                var schemeIndex = selection - 2;
                //// TODO: EXTERTNAL PROVIDER LOGIN
            }

            int GetSchemeSelection()
            {
                while (true)
                {
                    Console.WriteLine("\n\nWelcome to Feed Reader");
                    Console.WriteLine("Login using:");
                    int count = 1;
                    int selection = 0;
                    Console.WriteLine($"{count++}. Local account");
                    foreach (var scheme in schemes)
                    {
                        Console.WriteLine($"{count++}. {scheme}");
                    }
                    Console.Write("Your selection: ");
                    try
                    {
                        selection = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                    }
                    if (selection >= 1 && selection <= schemes.Length + 1)
                    {
                        return selection;
                    }
                    Console.WriteLine("Invalid selection!!!");
                }
            }
        }

        private async Task ExecuteFeedsAsync()
        {
            IEnumerable<FeedChannel> feedChannels;

            while (true)
            {
                var getAllFeedsResult = await _mediator.Send(new GetAllFeeds());
                if (getAllFeedsResult.Status != ResultStatus.Ok)
                {
                    _logger.LogError(string.Join(",", getAllFeedsResult.Errors));
                    return;
                }
                feedChannels = getAllFeedsResult.Value;
                var selection = GetFeedChannelSelection();
                if (selection == -1)
                {
                    //// TODO: ADD CHANNLE HERE
                    ///
                    continue;
                }

                await ExecuteFeedChannelAsync(feedChannels.ElementAt(selection-1));
            }

            int GetFeedChannelSelection()
            {
                while (true)
                {
                    Console.WriteLine("\n\n---- Feeds ---- (enter 'a' to add a new feed)");
                    int count = 1;
                    string selectionStr;
                    int selectionInt = 0;

                    foreach (var feedChannel in feedChannels)
                    {
                        Console.WriteLine($"{count}. {feedChannel.Title}");
                    }

                    Console.Write("Your selection: ");
                    selectionStr = Console.ReadLine();
                    if (selectionStr == "a")
                    {
                        return -1;
                    }
                    try
                    {
                        selectionInt = Convert.ToInt32(selectionStr);
                    }
                    catch
                    {
                    }
                    if (selectionInt >= 1 && selectionInt <= feedChannels.Count())
                    {
                        return selectionInt;
                    }
                    Console.WriteLine("Invalid selection!!!");
                }
            }
        }

        private async Task ExecuteFeedChannelAsync(FeedChannel feedChannel)
        {

        }


        //private void ShowFeedChannel(FeedChannel feedChannel)
        //{
        //    var msg = string.Empty;
        //    msg += Environment.NewLine;
        //    msg += $"======== FEED CHANNEL ========{Environment.NewLine}";
        //    msg += $"Title: {feedChannel.Title}{Environment.NewLine}";
        //    msg += $"Description: {feedChannel.Description}{Environment.NewLine}";
        //    msg += $"Link: {feedChannel.Link}{Environment.NewLine}";
        //    foreach (var feedItem in feedChannel.FeedItems)
        //    {
        //        msg += $"-------- FEED ITEM --------{Environment.NewLine}";
        //        msg += $"Title: {feedItem.Title}{Environment.NewLine}";
        //        msg += $"Description: {feedItem.Description}{Environment.NewLine}";
        //        msg += $"Link: {feedItem.Link}{Environment.NewLine}";
        //    }
        //    _logger.LogInformation(msg);
        //}
    }
}
