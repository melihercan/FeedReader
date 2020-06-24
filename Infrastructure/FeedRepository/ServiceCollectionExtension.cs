using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using MediatR;
using System.Reflection;
using System.Text;
using Application.Interfaces;
using Infrastructure;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection AddFeedRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IFeedRepository, FeedRepository>();
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
