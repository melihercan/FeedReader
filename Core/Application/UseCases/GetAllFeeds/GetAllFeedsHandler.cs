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
using Application.Helpers;
using System.Linq;

namespace Application.UseCases
{
    public class GetAllFeedsHandler : IRequestHandler<GetAllFeeds, Result<IEnumerable<FeedChannel>>>
    {
        private readonly ILogger<GetAllFeedsHandler> _logger;
        private readonly IRegistry _registry;

        public GetAllFeedsHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<GetAllFeedsHandler>>();
            _registry = ServiceCollectionExtension.ServiceProvider.GetService<IRegistry>();
        }

        public async Task<Result<IEnumerable<FeedChannel>>> Handle(GetAllFeeds request, 
            CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<FeedChannel>>();
            try
            {
                _logger.LogInformation($"{Utils.GetCurrentMethod()}");
                await Task.CompletedTask;
                result.Value = _registry.Feeds.Select(feed => feed.FeedChannel);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
