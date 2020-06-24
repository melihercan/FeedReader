using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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

                var feedChannel = await _feedSource.GetAsync(request.FeedChannel.Link);
////                feedChannel.FeedChannelId = request.FeedChannel.FeedChannelId;
////                feedChannel.ApplicationUsersLink = request.FeedChannel.ApplicationUsersLink;
////                var items = feedChannel.FeedItems.Select(newItem => 
////                {
////                    var oldItem = request.FeedChannel.FeedItems.SingleOrDefault(oldItem => oldItem.Link == newItem.Link);
////                    if (oldItem != null)
////                    {
////                        newItem = oldItem;
////                    }
////                    return newItem;
////                }).ToList();

////                feedChannel.FeedItems = items;
                await _feedRepository.UpdateFeedChannelAsync(request.FeedChannel.FeedChannelId, feedChannel);

                //// TODO: ADD NOTIFY FEED UPDATE


               return Result<FeedChannel>.Success(new FeedChannel());
            }
            catch (Exception ex)
            {
                return Result<FeedChannel>.Error(ex.Message);
            }

        }
    }
}
