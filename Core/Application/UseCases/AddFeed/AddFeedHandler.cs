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
using Infrastructure;
using Ardalis.Result;

namespace Application.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed, Result<FeedChannel>>
    {
        private readonly ILogger<AddFeedHandler> _logger;
        private readonly IRegistry _registry;
        private readonly IFeedRepository _feedRepository;
        private readonly IFeedSource _feedSource;

        public AddFeedHandler()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<AddFeedHandler>>();
            _registry = ServiceCollectionExtension.ServiceProvider.GetService<IRegistry>();
            _feedSource = ServiceCollectionExtension.ServiceProvider.GetService<IFeedSource>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();
        }

        public async Task<Result<FeedChannel>> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("start");

                FeedChannel feedChannel;
                var validator = new AddFeedValidator();
                var validationResult = validator.Validate(request);
                if (validationResult.IsValid)
                {
                    if(_registry.FeedChannels.FirstOrDefault(feedChannel => feedChannel.Link == request.Url) != null)
                    {
                        throw new Exception("Feed URL already exist");
                    }

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
                
                feedChannel.FeedItems.Select(item => item.FeedChannel = feedChannel).ToList();
                _registry.FeedChannels.Add(feedChannel);
                return Result<FeedChannel>.Success(feedChannel);
            }
            catch(Exception ex)
            {
                return Result<FeedChannel>.Error(ex.Message);
            }
            //await Task.CompletedTask;
        }
    }
}
