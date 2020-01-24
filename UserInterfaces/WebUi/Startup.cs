using Gateways.Identity;
using Gateways.Repository;
using Interactors.Feed.Commands.CreateFeed;
using Interactors.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace WebUi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateFeedCommand).GetTypeInfo().Assembly);

            services.AddSingleton<IIdentity, Identity>();
            services.AddSingleton<IFeedRepository, FeedRepository>();
            services.AddHostedService<Worker>();
            services.AddSingleton<IHostedService, Bootstrap>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
