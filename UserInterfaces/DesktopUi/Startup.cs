using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using WebUi;
using WebWindows.Blazor;


namespace DesktopUi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebUiServices();
            services.AddUserServices();
            services.AddFeedSourceServices();
            services.AddFeedRepositoryServices();

            // Do this after Infrastructure service inits.
            services.AddApplicationServices();
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
