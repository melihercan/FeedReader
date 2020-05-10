using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IRegistry, Registry>();
            services.AddSingleton<IFeedSource, FeedSource>();
            services.AddSingleton<IFeedRepository, FeedRepository>();
            services.AddSingleton<IUser, User>();
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
