using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Interactors.Interfaces;
using Interactors.Feed.Commands.CreateFeed;
using Gateways.Identity;
using System.Reflection;

namespace WebUi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddMediatR(typeof(CreateFeedCommand).GetTypeInfo().Assembly);

            builder.Services.AddSingleton<IFeedRepository, Gateways.FeedRepository.Lib.FeedRepository>();
            builder.Services.AddSingleton<IIdentity, Identity>();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
