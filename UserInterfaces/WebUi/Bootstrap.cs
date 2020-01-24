using Interactors.Interfaces;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebUi
{
    public class Bootstrap : IHostedService
    {
        private readonly ILogger<Bootstrap> _logger;
        private readonly IMediator _mediator;
        private readonly IFeedRepository _feedRepository;
        private readonly IIdentity _identity;

        public Bootstrap(ILogger<Bootstrap> logger, IMediator mediator, IFeedRepository feedRepository, IIdentity identity)
        {
            _logger = logger;
            _mediator = mediator;
            _feedRepository = feedRepository;
            _identity = identity;
            _logger.LogInformation("=============================EnBuyukFenerbahce2");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("=============================EnBuyukFenerbahce22");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
