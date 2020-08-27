using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using Application;
using WebUi;
using Infrastructure;
using Shared;

namespace WebUi.Client
{
    public class Program
    {
        private const string _webApiName = "Infrastructure.ServerAPI";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient(_webApiName, client => client.BaseAddress = 
                new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient(_webApiName));
            builder.Services.AddApiAuthorization();

            builder.Services.AddWebUiServices();
            builder.Services.AddUserAccount();
            builder.Services.AddFeedSource();
            builder.Services.AddFeedRepository();
            builder.Services.AddTokenRepository();
            
            // Do this after Infrastructure service inits.
            builder.Services.AddApplication();

            var host = builder.Build();
            Registry.ServiceProvider = builder.Services.BuildServiceProvider();
            await host.RunAsync();
        }
    }
}
