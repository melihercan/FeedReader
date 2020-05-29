using Microsoft.Extensions.DependencyInjection;
using WebUi;
using WebWindows.Blazor;


namespace DesktopUi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
