using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Reactive.Threading.Tasks;

namespace Application.UseCases
{
    public class NotifyFeedUpdateHandler : IRequestHandler<NotifyFeedUpdate, Result<IObservable<FeedChannel>>> 
    {
        private readonly ILogger<NotifyFeedUpdateHandler> _logger;
        private readonly IMediator _mediator;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(15);
        private readonly TimeSpan _updateInterval = TimeSpan.FromMinutes(30);

        public NotifyFeedUpdateHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<NotifyFeedUpdateHandler>>();
            _mediator = ServiceCollectionExtension.ServiceProvider.GetService<IMediator>();
        }

        public Task<Result<IObservable<FeedChannel>>> Handle(NotifyFeedUpdate request, 
            CancellationToken cancellationToken)
        {
            var obFeedChannel = Observable
                .Interval(_checkInterval)
                .Select(async _ => (await GetUpdatedFeedChannelsAsync()).ToObservable())
                .Concat()
                .Concat();

            return Task.FromResult(Result<IObservable<FeedChannel>>.Success(obFeedChannel));
        }
        private async Task<IEnumerable<FeedChannel>> GetUpdatedFeedChannelsAsync()
        {
            Console.WriteLine("#### Checking updates...");

            List<FeedChannel> updatedFeedChannels = new List<FeedChannel>();

            var getResult = await _mediator.Send(new GetAllFeeds { });
            if (getResult.Status == ResultStatus.Ok)
            {
                var now = DateTime.Now;
                var feedChannels = getResult.Value.ToList();
                foreach (var feedChannel in feedChannels)
                {
                    if (now - feedChannel.LastUpdateTime > _updateInterval)
                    {
                        await Task.Delay(1000);
                        var updateResult = await _mediator.Send(new UpdateFeed
                        {
                            FeedChannel = feedChannel
                        });
                        if (updateResult.Status == ResultStatus.Ok)
                        {
                            updatedFeedChannels.Add(updateResult.Value);
                        }
                    }
                }
            }

            Console.WriteLine($"#### {updatedFeedChannels.Count()} channel updates...");
            return updatedFeedChannels;
        }
    }
}
