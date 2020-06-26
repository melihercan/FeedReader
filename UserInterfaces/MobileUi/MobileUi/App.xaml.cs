using System;
using Xamarin.Forms;
using MobileUi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure;
using Application;

namespace MobileUi
{
    public partial class App : Xamarin.Forms.Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static App PreInit(Action<IServiceCollection> platformConfigureServices)
        {
            //var systemDir = FileSystem.CacheDirectory;
            //Utils.ExtractSaveResource("Bridge.appsettings.json", systemDir);
            //var fullConfig = Path.Combine(systemDir, "Bridge.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(context =>
                {
                    //context.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    //context.AddJsonFile(fullConfig);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<AppShell>();
                    services.AddSingleton<App>();

                    services.AddUserServices();
                    services.AddFeedSourceServices();
                    services.AddFeedRepositoryServices();

                    // Do this after Infrastructure service inits.
                    services.AddApplicationServices();

                    platformConfigureServices(services);
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                })
                .Build();

            ServiceProvider = host.Services;
            return ServiceProvider.GetService<App>();

        }

        public void PostInit()
        {

        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
