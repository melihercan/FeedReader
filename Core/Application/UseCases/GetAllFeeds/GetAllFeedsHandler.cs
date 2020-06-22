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
using Ardalis.Result;
using Infrastructure;

namespace Application.UseCases
{
    public class GetAllFeedsHandler : IRequestHandler<GetAllFeeds, Result<IEnumerable<FeedChannel>>>
    {
        private readonly ILogger<GetAllFeedsHandler> _logger;
        private readonly IRegistry _registry;
        private readonly IFeedRepository _feedRepository;

        public GetAllFeedsHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<GetAllFeedsHandler>>();
            _registry = ServiceCollectionExtension.ServiceProvider.GetService<IRegistry>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();
        }

        public async Task<Result<IEnumerable<FeedChannel>>> Handle(GetAllFeeds request, 
            CancellationToken cancellationToken)
        {
            Result<IEnumerable<FeedChannel>> result;
            try
            {
                _logger.LogInformation($"{Utils.GetCurrentMethod()}");
                //result = Result<IEnumerable<FeedChannel>>.Success(_registry.FeedChannels);
                result = Result<IEnumerable<FeedChannel>>.Success(await _feedRepository.GetFeedChannelsAsync());
            }
            catch (Exception ex)
            {
                result = Result<IEnumerable<FeedChannel>>.Error(ex.Message);
            }
            await Task.CompletedTask;
            return result;
        }
    }
}
