using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace Application.Services
{
    public class FeedUpdater
    {
        private readonly ILogger<FeedUpdater> _logger;
        private readonly IFeedRepository _feedRepository;

        public FeedUpdater()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<FeedUpdater>>();
            _feedRepository = ServiceCollectionExtension.ServiceProvider.GetService<IFeedRepository>();

            //Observable.FromAsync()

            new Timer((_) => {
                Console.WriteLine($"============================ Timer tick: {DateTime.Now}");
            }, null, 0, 10000);

        }
    }
}
