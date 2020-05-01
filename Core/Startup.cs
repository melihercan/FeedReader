using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Startup
    {
        static Startup()
        {
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
                            })
                            .ConfigureLogging((context, builder) =>
                            {
                                builder.AddConsole();
                            })
                            .Build();
        }
    }
}
