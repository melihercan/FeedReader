using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection AddFeedSourceServices(this IServiceCollection services)
        {
            services.AddMediatR(new Assembly[]
            {
                typeof(FeedSource).Assembly,
            });

            services.AddSingleton<IFeedSource, FeedSource>();
            ServiceProvider = services .BuildServiceProvider();
            return services;
        }

    }
}
