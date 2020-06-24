using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Reflection;

namespace Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
////            services.AddFeedSourceServices();
////            services.AddFeedRepositoryServices();

            services.AddMediatR(new Assembly[] 
            {
                typeof(ServiceCollectionExtension).Assembly,
            });

            services.AddSingleton<IRegistry, Registry>();
////            services.AddSingleton<IUser, User>();
            services.AddSingleton<IFeedManager, FeedManager>();

            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
