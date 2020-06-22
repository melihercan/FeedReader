using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using MediatR;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection AddFeedRepositoryServices(this IServiceCollection services)
        {
            services.AddMediatR(new Assembly[]
            {
                typeof(FeedRepository).Assembly,
            });

            services.AddSingleton<IFeedRepository, FeedRepository>();
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
