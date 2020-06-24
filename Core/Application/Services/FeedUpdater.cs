using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using MediatR;
using Application.UseCases;
using Ardalis.Result;
using System.Linq;

namespace Application.Services
{
    public class FeedUpdater
    {
        private readonly ILogger<FeedUpdater> _logger;
        private readonly IMediator _mediator;
        private const int Interval = 15 * 1000*60; 
        private readonly TimeSpan _timeSpan = TimeSpan.FromMinutes(30);

        public FeedUpdater()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<FeedUpdater>>();
            _mediator = ServiceCollectionExtension.ServiceProvider.GetService<IMediator>();

            //Observable.FromAsync()

            new Timer(async(_) => 
            {
                Console.WriteLine("Checking updates...");

                var getResult = await _mediator.Send(new GetAllFeeds { });
                if (getResult.Status == ResultStatus.Ok)
                {
                    var now = DateTime.Now;
                    var feedChannels = getResult.Value.ToList();
                    foreach (var feedChannel in feedChannels)
                    {
                        if(now - feedChannel.LastUpdateTime > _timeSpan)
                        {
                            await Task.Delay(1000);
                            var updateResult = await _mediator.Send(new UpdateFeed
                            {
                                FeedChannel = feedChannel
                            });
                            if (updateResult.Status == ResultStatus.Ok)
                            {
                                var newFeedChannel = updateResult.Value;

                                //// TODO: INVOKE Notfiy Feed update
                            }
                        }
                    }
                }

            }, null, Interval, Interval);
        }
    }
}
