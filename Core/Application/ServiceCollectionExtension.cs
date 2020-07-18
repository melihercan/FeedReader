using Application.Interfaces;
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

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(new Assembly[] 
            {
                typeof(ServiceCollectionExtension).Assembly,
            });


            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
