using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using System.Linq;

namespace Application.UseCases
{
    public class GetAllFeedsHandler : IRequestHandler<GetAllFeeds, IEnumerable<FeedChannel>>
    {
        private readonly ILogger<GetAllFeedsHandler> _logger;
        private readonly IRegistry _registry;

        public GetAllFeedsHandler()
        {
            _logger = ModuleInitializer.ServiceProvider.GetService<ILogger<GetAllFeedsHandler>>();
            _registry = ModuleInitializer.ServiceProvider.GetService<IRegistry>();
        }

        public async Task<IEnumerable<FeedChannel>> Handle(GetAllFeeds request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("============== GetAllFeedsHandler");
            
            await Task.CompletedTask;

            return _registry.Feeds.Select(feed => feed.FeedChannel);

        }
    }


}
