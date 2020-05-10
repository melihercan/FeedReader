using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases
{
    public class RemoveAllFeedsHandler : IRequestHandler<RemoveAllFeeds>
    {
        private readonly ILogger<RemoveAllFeedsHandler> _logger;

        public RemoveAllFeedsHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<RemoveAllFeedsHandler>>();
        }

        public async Task<Unit> Handle(RemoveAllFeeds request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("============== RemoveAllFeedsHandler");
            await Task.CompletedTask;
            return new Unit();
        }
    }
}
