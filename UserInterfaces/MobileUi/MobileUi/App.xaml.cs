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

        //public static App PreInit(Action<IServiceCollection> platformConfigureServices)
        //{
        //    //var systemDir = FileSystem.CacheDirectory;
        //    //Utils.ExtractSaveResource("Bridge.appsettings.json", systemDir);
        //    //var fullConfig = Path.Combine(systemDir, "Bridge.appsettings.json");

        //    var hostBuilder = Device.RuntimePlatform == Device.Android ? Host.CreateDefaultBuilder() : new HostBuilder();

        //    var host = hostBuilder
        //        ////                .ConfigureHostConfiguration(context =>
        //        ////            {
        //        //context.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
        //        //context.AddJsonFile(fullConfig);
        //        ////        })
        //        .ConfigureServices((context, services) =>
        //        {
        //            var configuration = context.Configuration;

        //            services.AddHttpClient(_webApiName, client => client.BaseAddress =
        //                new Uri(configuration["Server:URL"]));
        //            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
        //                .CreateClient(_webApiName));

        //            services.AddSingleton<AppShell>();
        //            services.AddSingleton<App>();

        //            services.AddUser();
        //            services.AddFeedSource();
        //            services.AddFeedRepository();
        //            services.AddTokenRepository();

        //            // Do this after Infrastructure service inits.
        //            services.AddApplication();

        //            platformConfigureServices(services);
        //        })
        //        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
        //        {
        //            ////hostBuilderContext.HostingEnvironment.SetUi(Ui.Mobile);
        //        })
        //        .ConfigureLogging((context, builder) =>
        //        {
        //            builder.AddConsole();
        //        })
        //        .Build();

        //    Registry.ServiceProvider = host.Services;

        //    return Registry.ServiceProvider.GetService<App>();

        //}

        //public void PostInit()
        //{

        //}

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
