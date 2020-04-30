using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.UseCases;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleUi
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;
        private readonly IFeedRepository _feedRepository;
        private readonly IIdentity _identity;

        public Worker(ILogger<Worker> logger, IMediator mediator, IFeedRepository feedRepository, IIdentity identity)
        {
            _logger = logger;
            _mediator = mediator;
            _feedRepository = feedRepository;
            _identity = identity;

////            Interactors.Container.Init(_feedRepository, _identity);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ////Interactors.Container.Init(_feedRepository, _identity);

            await _mediator.Send(new AddFeed
            {
                 Url = "https://www.melihercan.org"
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
