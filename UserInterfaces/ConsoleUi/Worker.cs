using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly HttpClient _httpClient;

        public Worker(ILogger<Worker> logger, IMediator mediator, HttpClient httpClient)
        {
            _logger = logger;
            _mediator = mediator;
            _httpClient = httpClient;
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
            if (selection == 1)
            {
                Console.Write("\nUsername: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                Console.WriteLine("Logging in...");
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
                    Console.Write("\nFeed URL: ");
                    var url = Console.ReadLine();
                    var feedResult = await _mediator.Send(new AddFeed
                    {
                        Url = url
                    });
                    if (feedResult.Status == ResultStatus.Ok)
                    {
                    }
                    else
                    {
                        _logger.LogError(string.Join(",", feedResult.Errors));
                    }
                    continue;
                }

                await ExecuteFeedChannelAsync(feedChannels.ElementAt(selection - 1));
            }

            int GetFeedChannelSelection()
            {
                while (true)
                {
                    Console.WriteLine("\n\n#### Feeds #### (enter 'a' to add a new feed)");
                    int count = 1;
                    string selectionStr;
                    int selectionInt = 0;

                    foreach (var feedChannel in feedChannels)
                    {
                        Console.WriteLine($"{count++}. {feedChannel.Title}");
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
            IEnumerable<FeedItem> feedItems;

            while (true)
            {
                feedItems = feedChannel.FeedItems.OrderByDescending(feedItem => feedItem.PublishDate);
                var selection = GetFeedItemSelection();
                if (selection == -1)
                {
                    // Go back.
                    break;
                }
                else if (selection == -2)
                {
                    // Update items.
                    var feedResult = await _mediator.Send(new UpdateFeed
                    {
                        FeedChannel = feedChannel
                    });
                    if (feedResult.Status == ResultStatus.Ok)
                    {
                        feedChannel = feedResult.Value;
                    }
                    else
                    {
                        _logger.LogError(string.Join(",", feedResult.Errors));
                    }

                    continue;
                }
                else if (selection == -3)
                {
                    // Remove channel.
                    var feedResult = await _mediator.Send(new RemoveFeed
                    {
                        Id = feedChannel.FeedChannelId
                    });
                    if (feedResult.Status == ResultStatus.Ok)
                    {
                    }
                    else
                    {
                        _logger.LogError(string.Join(",", feedResult.Errors));
                    }
                    break;
                }
                else
                {
                    // Get item page (HTML) and convert to text.
                    //var response = await _httpClient.GetAsync(feedItems.ElementAt(selection - 1).Link);
                    //if (!response.IsSuccessStatusCode)
                    //{
                    //    Console.WriteLine("Couldn't get valid HTTP response!!!");
                    //    break;
                    //}
                    //var html = await response.Content.ReadAsStringAsync();

                    // For now print description and link.
                    var feedItem = feedItems.ElementAt(selection - 1);
                    Console.WriteLine($"\n\n#### {feedItem.Title} #### " +
                        $"(hit any key to return)");
                    Console.WriteLine($"{feedItem.Description}");
                    Console.WriteLine($"{feedItem.PublishDate}");
                    Console.WriteLine($"URL: {feedItem.Link}");
                    Console.ReadKey();
                }
            }

            int GetFeedItemSelection()
            {
                while (true)
                {
                    Console.WriteLine($"\n\n#### {feedChannel.Title} #### " +
                        $"(enter 'b' to go back, 'u' to update, 'r' to remove)");
                    int count = 1;
                    string selectionStr;
                    int selectionInt = 0;

                    foreach (var feedItem in feedItems)
                    {
                        Console.WriteLine($"{count++}. {feedItem.Title} - {feedItem.PublishDate}");
                    }

                    Console.Write("Your selection: ");
                    selectionStr = Console.ReadLine();
                    selectionInt = selectionStr switch
                    {
                        "b" => -1,
                        "u" => -2,
                        "r" => -3,
                        _ => 0,
                    };
                    if (selectionInt != 0)
                    {
                        return selectionInt;
                    }
                    try
                    {
                        selectionInt = Convert.ToInt32(selectionStr);
                    }
                    catch
                    {
                    }
                    if (selectionInt >= 1 && selectionInt <= feedItems.Count())
                    {
                        return selectionInt;
                    }
                    Console.WriteLine("Invalid selection!!!");
                }
            }

        }

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
