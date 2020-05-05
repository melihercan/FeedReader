using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UseCases;
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
            try
            {
                var feedChannel = await _mediator.Send(new AddFeed
                {
                    Url = "https://www.cnbc.com/id/100003114/device/rss/rss.html"
                });

                var feeds = await _mediator.Send(new GetAllFeeds { });
                foreach (var feed in feeds)
                {
                    _logger.LogInformation($"{feed.Title}, {feed.Description}, {feed.Link}");
                    for(int i=0; i< feed.FeedItems.Count; i++)
                    {
                        _logger.LogInformation($"{feed.FeedItems[i].Title}");
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            //while (!stoppingToken.IsCancellationRequested)
            //{
              //  _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
