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
using System.ServiceModel.Syndication;

namespace Application.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed, Result<FeedChannel>>
    {
        private readonly ILogger<AddFeedHandler> _logger;
        private readonly IRegistry _registry;

        public AddFeedHandler()
        {
            _logger = ModuleInitializer.ServiceProvider.GetService<ILogger<AddFeedHandler>>();
            _registry = ModuleInitializer.ServiceProvider.GetService<IRegistry>();
        }

        async Task<Result<FeedChannel>> IRequestHandler<AddFeed, Result<FeedChannel>>.Handle(AddFeed request, CancellationToken cancellationToken)
        {
            var result = new Result<FeedChannel>();

            try
            {
                _logger.LogInformation($"{Utils.GetCurrentMethod()}");

                Feed feed;
                var validator = new AddFeedValidator();
                var validationResult = validator.Validate(request);
                if (validationResult.IsValid)
                {
                    if(_registry.Feeds.FirstOrDefault(feed => feed.FeedChannel.Link == request.Url) != null)
                    {
                        throw new Exception("Feed URL alread exist");
                    }

                    try
                    {
                        var syndicationFeed = SyndicationFeed.Load(XmlReader.Create(request.Url));
                        feed = new Feed 
                        { 
                             Id = 0,
                             FeedChannel = new FeedChannel
                             {
                                 Id = 0,
                                 Title = syndicationFeed.Title.Text,
                                 Description = syndicationFeed.Description.Text,
                                 Link = syndicationFeed.Links[0].Uri.AbsoluteUri,
                                 FeedItems = syndicationFeed.Items.Select(item => new FeedItem 
                                 { 
                                     Id = 0,
                                     Title = item.Title.Text,
                                     Description = item.Summary.Text,
                                     Link = item.Links[0].Uri.AbsoluteUri,
                                 }).ToList()
                             },
                             SyndicationFeed = syndicationFeed
                        };
                    }
                    catch
                    {
                        throw new Exception("Invalid feed URL");
                    }
                }
                else
                {
                    throw new Exception(
                        string.Join(',', validationResult.Errors.Select(error => error.ErrorMessage)));
                }

                await Task.CompletedTask;
                
                feed.FeedChannel.FeedItems.Select(item => item.FeedChannel = feed.FeedChannel).ToList();
                _registry.Feeds.Add(feed);
                result.Value = feed.FeedChannel;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
