using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Application.Services;
using Infrastructure;

namespace Application
{
    public static class ModuleInitializer
    {
        public static System.Reflection.Assembly Assembly => typeof(ModuleInitializer).Assembly;

        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Initialize()
        {
            Console.WriteLine("Hello from Core");

            var host = new HostBuilder()
                            .ConfigureHostConfiguration(context =>
                            {
                            })
                            .ConfigureServices((context, services) =>
                            {
                                services.AddSingleton<IRegistry, Registry>();
                                services.AddSingleton<IFeedRepository, FeedRepository>();
                                services.AddSingleton<IIdentity, Identity>();
                            })
                            .ConfigureLogging((context, builder) =>
                            {
                                builder.AddConsole();
                            })
                            .Build();

            ServiceProvider = host.Services;

        }
    }

}
