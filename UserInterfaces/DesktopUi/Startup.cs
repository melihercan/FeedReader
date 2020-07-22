using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;
using System;
using System.Net.Http;
using WebUi;
using WebWindows.Blazor;


namespace DesktopUi
{
    public class Startup
    {
        private const string _webApiName = "Infrastructure.ServerAPI";

        public void ConfigureServices(IServiceCollection services)
        {
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            services.AddHttpClient(_webApiName, client => client.BaseAddress =
                new Uri(configuration["Server:URL"]));
            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient(_webApiName));

            ////services.AddApiAuthorization();


            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            services.AddSingleton<SignOutSessionStateManager>();


            services.AddWebUiServices();
            services.AddUser();
            services.AddFeedSource();
            services.AddFeedRepository();
            services.AddTokenRepository();

            // Do this after Infrastructure service inits.
            services.AddApplication();
        }



        public void Configure(DesktopApplicationBuilder app)
        {
            Registry.ServiceProvider = app.Services;
            app.AddComponent<App>("app");

        }
    }
}
