using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Application;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared;

namespace ConsoleUi
{
    public class Program
    {
        private const string _webApiName = "Infrastructure.ServerAPI";

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
                    services.AddHttpClient(_webApiName, client => client.BaseAddress =
                        new Uri(services.BuildServiceProvider().GetRequiredService<IConfiguration>()["Server:URL"]));
                    services.AddSingleton/*AddTransient*/(sp => sp.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(_webApiName));

                    services.AddUserAccount();
                    services.AddFeedSource();
                    services.AddFeedRepository();
                    services.AddTokenRepository();

                    // Do this after Infrastructure service inits.
                    services.AddApplication();

                    services.AddHostedService<Worker>();
                })
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                });
    }
}
