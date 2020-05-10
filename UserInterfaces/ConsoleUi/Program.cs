using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleUi
{
    public class Program
    {
        public static /*async Task*/ void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

#if true
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplicationServices();
                    services.AddMediatR(new Assembly[] { typeof(ServiceCollectionExtension).Assembly });

                    services.AddHostedService<Worker>();
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                });
#endif
    }
}
