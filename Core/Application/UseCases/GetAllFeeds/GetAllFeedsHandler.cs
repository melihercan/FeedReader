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
using Ardalis.Result;

namespace Application.UseCases
{
    public class GetAllFeedsHandler : IRequestHandler<GetAllFeeds, Result<IEnumerable<FeedChannel>>>
    {
        private readonly ILogger<GetAllFeedsHandler> _logger;
        private readonly IFeedRepository _feedRepository;

        public GetAllFeedsHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<GetAllFeedsHandler>>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();
        }

        public async Task<Result<IEnumerable<FeedChannel>>> Handle(GetAllFeeds request, 
            CancellationToken cancellationToken)
        {
            try
            {
                return Result<IEnumerable<FeedChannel>>.Success(await _feedRepository.GetFeedChannelsAsync());
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<FeedChannel>>.Error(ex.Message);
            }
        }
    }
}
