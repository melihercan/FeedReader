using Application;
using Infrastructure;
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
