using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;
using WebUi;
using WebWindows.Blazor;


namespace DesktopUi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthenticationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            ////services.AddAuthorization();
            services.AddApiAuthorization();

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
