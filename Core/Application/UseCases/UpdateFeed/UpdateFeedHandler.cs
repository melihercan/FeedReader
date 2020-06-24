using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Application.Interfaces;

namespace Application.UseCases
{
    public class UpdateFeedHandler : IRequestHandler<UpdateFeed, Result<FeedChannel>>
    {
        private readonly ILogger<UpdateFeedHandler> _logger;
        private readonly IFeedRepository _feedRepository;
        private readonly IFeedSource _feedSource;

        public UpdateFeedHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<UpdateFeedHandler>>();
            _feedSource = ServiceCollectionExtension.ServiceProvider.GetService<IFeedSource>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();
        }

        public async Task<Result<FeedChannel>> Handle(UpdateFeed request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug($"{Utils.GetCurrentMethod()}");

                // Get the latest from source.
                var feedChannel = await _feedSource.GetAsync(request.FeedChannel.Link);

                // Update in repository.
                await _feedRepository.UpdateFeedChannelAsync(request.FeedChannel.FeedChannelId, feedChannel);

                // Get the updated from repository.
                feedChannel = await _feedRepository.GetFeedChannelAsync(request.FeedChannel.FeedChannelId);

                return Result<FeedChannel>.Success(feedChannel);
            }
            catch (Exception ex)
            {
                return Result<FeedChannel>.Error(ex.Message);
            }

        }
    }
}
