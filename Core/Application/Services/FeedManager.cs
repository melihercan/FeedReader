using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class FeedManager : IFeedManager
    {
        private readonly ILogger<FeedManager> _logger;
        private readonly IFeedRepository _feedRepository;

        public FeedManager()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<FeedManager>>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();

            //Observable.FromAsync()
        }

        public Task<IEnumerable<FeedChannel>> GetFeedChannels()
        {
            throw new NotImplementedException();
        }
    }
}
