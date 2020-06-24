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

namespace WebUi.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("WebUi.ServerAPI", client => client.BaseAddress = 
                new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("WebUi.ServerAPI"));
            builder.Services.AddApiAuthorization();

            builder.Services.AddWebUiServices();
            builder.Services.AddUserServices();
            builder.Services.AddFeedSourceServices();
            builder.Services.AddFeedRepositoryServices();
            
            // Do this after Infrastructure service inits.
            builder.Services.AddApplicationServices();

            await builder.Build().RunAsync();
        }
    }
}
