using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.Interfaces;

namespace Core
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
                                //context.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                                //context.AddJsonFile(fullConfig);
                            })
                            .ConfigureServices((context, services) =>
                            {
                                //                                services.AddSingleton<ITagConfigurationViewModel, TagConfigurationViewModel>();
                                ////                    services.AddSingleton<TagConfigurationPage>();
                                //                  services.AddSingleton<IMessageManager, MessageManager>();
                                //                            services.AddSingleton<AppShell>();
                                //                          services.AddSingleton<App>();

                                //                        platformConfigureServices(services);
                                services.AddSingleton<IRegistry, Registry>();
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
