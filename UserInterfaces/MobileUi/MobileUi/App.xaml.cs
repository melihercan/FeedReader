using System;
using Xamarin.Forms;
using MobileUi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure;
using Application;
using Shared;

namespace MobileUi
{
    public partial class App : Xamarin.Forms.Application
    {
        public static App PreInit(Action<IServiceCollection> platformConfigureServices)
        {
            //var systemDir = FileSystem.CacheDirectory;
            //Utils.ExtractSaveResource("Bridge.appsettings.json", systemDir);
            //var fullConfig = Path.Combine(systemDir, "Bridge.appsettings.json");

            var hostBuilder = Device.RuntimePlatform == Device.Android ? Host.CreateDefaultBuilder() : new HostBuilder();


            var host = hostBuilder
                ////                .ConfigureHostConfiguration(context =>
                ////            {
                //context.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                //context.AddJsonFile(fullConfig);
                ////        })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<AppShell>();
                    services.AddSingleton<App>();

                    services.AddUser();
                    services.AddFeedSource();
                    services.AddFeedRepository();
                    services.AddTokenRepository();

                    // Do this after Infrastructure service inits.
                    services.AddApplication();

                    platformConfigureServices(services);
                })
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                    ////hostBuilderContext.HostingEnvironment.SetUi(Ui.Mobile);
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                })
                .Build();

            Registry.ServiceProvider = host.Services;

            return Registry.ServiceProvider.GetService<App>();

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
