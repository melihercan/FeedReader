using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTokenRepository(this IServiceCollection services)
        {
Console.WriteLine("******************************* Token Repository AddTokenRepository");

            services.AddSingleton<ITokenRepository, TokenRepository>();
            return services;
        }
    }
}
