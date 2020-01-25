using Gateways.Identity;
using Gateways.Repository;
using Interactors.Feed.Commands.CreateFeed;
using Interactors.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MobileUi
{
    public class Bootstrap
    {
        public static App Init()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMediatR(typeof(CreateFeedCommand).GetTypeInfo().Assembly);
                    services.AddSingleton<IFeedRepository, FeedRepository>();
                    services.AddSingleton<IIdentity, Identity>();

                    services.AddSingleton<App>();
                })
                .Build();

            //var x = host.Services.GetService<FeedRepository>();
            //var y = host.Services.GetService<Identity>();
            //var z = host.Services.GetService<App>();

            //Interactors.Container.Init(x, y);
            //                host.Services.GetService<FeedRepository>(),
            //              host.Services.GetService<Identity>());



            return host.Services.GetService<App>();
        }
    }
}
