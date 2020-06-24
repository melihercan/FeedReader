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

namespace Application.Services
{
    public class FeedUpdater : BackgroundService
    {
        private readonly ILogger<FeedUpdater> _logger;
        private readonly IFeedRepository _feedRepository;

        public FeedUpdater()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<FeedUpdater>>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();

            //Observable.FromAsync()
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
