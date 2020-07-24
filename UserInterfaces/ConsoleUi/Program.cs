using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared;

namespace ConsoleUi
{
    public class Program
    {
        private static IHostEnvironment Env;

        public static /*async Task*/ void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();
            Registry.ServiceProvider = hostBuilder.Services;
            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddUser();
                    services.AddFeedSource();
                    services.AddFeedRepository();
                    services.AddTokenRepository();

                    // Do this after Infrastructure service inits.
                    services.AddApplication();

                    services.AddHostedService<Worker>();
                })
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                    ////hostBuilderContext.HostingEnvironment.SetUi(Ui.Console);
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                });
    }
}
