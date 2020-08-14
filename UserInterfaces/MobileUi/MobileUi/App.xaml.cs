using System;
using Xamarin.Forms;
using MobileUi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure;
using Application;
using Shared;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Xamarinme;
using System.Reflection;

namespace MobileUi
{
    public partial class App : Xamarin.Forms.Application
    {
        private const string _webApiName = "Infrastructure.ServerAPI";

        public App()
        {
            InitializeXamarinHostBuilder();
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        private void InitializeXamarinHostBuilder()
        {
            var hostBuilder = XamarinHostBuilder.CreateDefault(new EmbeddedResourceConfigurationOptions 
            { 
                Assembly = Assembly.GetExecutingAssembly(),
                Prefix = "MobileUi"
            });

            hostBuilder.Services.AddHttpClient(_webApiName, client => client.BaseAddress =
                new Uri(hostBuilder.Configuration["Server:URL"]));
            hostBuilder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient(_webApiName));

            hostBuilder.Services.AddUser();
            hostBuilder.Services.AddFeedSource();
            hostBuilder.Services.AddFeedRepository();
            hostBuilder.Services.AddTokenRepository();

            // Do this after Infrastructure service inits.
            hostBuilder.Services.AddApplication();

            var host = hostBuilder.Build();
            Registry.ServiceProvider = host.Services;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
