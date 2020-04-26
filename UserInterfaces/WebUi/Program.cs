using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Interactors.Interfaces;
using Interactors.Feed.Commands.CreateFeed;
using Gateways.Identity;
using System.Reflection;
using Blazored.Modal;
using Blazored.Modal.Services;

namespace WebUi
{

    //https://proandroiddev.com/clean-architecture-data-flow-dependency-rule-615ffdd79e29
    //https://medium.com/@mr.anmolsehgal/clean-architecture-fef10b093ad0
    //  Entities, UseCases, Presenters, Gateways, Controllers
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddMediatR(typeof(CreateFeedCommand).GetTypeInfo().Assembly);
            builder.Services.AddBlazoredModal();

            builder.Services.AddSingleton<IFeedRepository, Gateways.FeedRepository.Lib.FeedRepository>();
            builder.Services.AddSingleton<IIdentity, Identity>();
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                options.ProviderOptions.Authority = "https://login.microsoftonline.com/";
                options.ProviderOptions.ClientId = "33333333-3333-3333-33333333333333333";
            });

            await builder.Build().RunAsync();
        }
    }
}
