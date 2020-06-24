using Application.Interfaces;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
////            services.AddMediatR(new Assembly[]
////            {
////                typeof(User).Assembly,
////            });

            services.AddSingleton<IUser, User>();
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
