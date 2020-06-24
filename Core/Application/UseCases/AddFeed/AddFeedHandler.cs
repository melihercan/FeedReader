using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Reflection;
using Application.Helpers;
using System.Linq;
using System.Xml;
using System.Net.Http;
using Ardalis.Result;

namespace Application.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed, Result<FeedChannel>>
    {
        private readonly ILogger<AddFeedHandler> _logger;
        private readonly IFeedRepository _feedRepository;
        private readonly IFeedSource _feedSource;

        public AddFeedHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<AddFeedHandler>>();
            _feedSource = ServiceCollectionExtension.ServiceProvider.GetService<IFeedSource>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();
        }

        public async Task<Result<FeedChannel>> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("start");

                FeedChannel feedChannel;
                var validator = new AddFeedValidator();
                var validationResult = validator.Validate(request);
                if (validationResult.IsValid)
                {
                    try
                    {
                        feedChannel = await _feedSource.GetAsync(request.Url);
                        _logger.LogInformation($"---- num of items: {feedChannel.FeedItems.Count()}");
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Invalid feed URL" + Environment.NewLine + ex.Message);
                    }
                }
                else
                {
                    throw new Exception(
                        string.Join(',', validationResult.Errors.Select(error => error.ErrorMessage)));
                }
                
                await _feedRepository.AddFeedChannelAsync(feedChannel);
                return Result<FeedChannel>.Success(feedChannel);
            }
            catch(Exception ex)
            {
                return Result<FeedChannel>.Error(ex.Message);
            }
        }
    }
}
