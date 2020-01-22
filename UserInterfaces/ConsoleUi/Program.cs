using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Gateways.Identity;
using Gateways.Repository;
using Interactors;
using Interactors.Feed.Commands.CreateFeed;
using Interactors.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleUi
{
    public class Program
    {
        public static /*async Task*/ void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            ///            

#if false
            var host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    //services.AddMediatR(typeof(CreateFeedCommand).GetTypeInfo().Assembly);


                    services.AddSingleton<IIdentity, Identity>();
                    services.AddSingleton<IFeedRepository, FeedRepository>();

                    services.AddSingleton<Interactors.Startup>();

                    services.AddHostedService<Worker>();
                });

            await host.RunConsoleAsync();
#endif

        }

#if true
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    // Add MediatR for each assembly that uses it.
                    ////var assemblies = new Assembly[] 
                    ////{ 
                    ////Assembly.GetExecutingAssembly(),

                    ///};
                    ////services.AddMediatR(Assembly.GetExecutingAssembly());



                    services.AddMediatR(typeof(CreateFeedCommand).GetTypeInfo().Assembly);


                    services.AddSingleton<IIdentity, Identity>();
                    services.AddSingleton<IFeedRepository, FeedRepository>();
                    services.AddHostedService<Worker>();

////                    var provider = services.BuildServiceProvider();



                });
#endif
    }
}
