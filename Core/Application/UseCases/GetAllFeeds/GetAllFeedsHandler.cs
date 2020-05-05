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

namespace Application.UseCases
{
    public class GetAllFeedsHandler : IRequestHandler<GetAllFeeds, IEnumerable<FeedChannel>>
    {
        private readonly ILogger<GetAllFeedsHandler> _logger;

        public GetAllFeedsHandler()
        {
            _logger = ModuleInitializer.ServiceProvider.GetService<ILogger<GetAllFeedsHandler>>();
        }

        public async Task<IEnumerable<FeedChannel>> Handle(GetAllFeeds request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("============== GetAllFeedsHandler");
            await Task.CompletedTask;
            return null;

        }
    }


}
