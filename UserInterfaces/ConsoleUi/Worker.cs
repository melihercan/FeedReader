using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Interactors.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleUi
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFeedRepository _feedRepository;
        private readonly IIdentity _identity;

        public Worker(ILogger<Worker> logger, IFeedRepository feedRepository, IIdentity identity)
        {
            _logger = logger;
            _feedRepository = feedRepository;
            _identity = identity;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Interactors.Gateways.Init(_feedRepository, _identity);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
