﻿using System;
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
using Application.Interfaces;
using MediatR;
using Application.UseCases;

namespace MobileUi
{
    public partial class App : Xamarin.Forms.Application
    {
        private const string _webApiName = "Infrastructure.ServerAPI";

        public App()
        {
            InitializeXamarinHostBuilder();
            InitializeComponent();

            //var tokenRepository = Registry.ServiceProvider.GetService<ITokenRepository>();
            //var token = tokenRepository.RetrieveAsync();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        private void InitializeXamarinHostBuilder()
        {
            var hostBuilder = XamarinHostBuilder.CreateDefault(new EmbeddedResourceConfigurationOptions 
            { 
                Assembly = Assembly.GetExecutingAssembly(),
                Prefix = "MobileUi"
            });

            ////hostBuilder.Services.AddHttpClient(_webApiName, client => client.BaseAddress =
            ////                new Uri(hostBuilder.Configuration["Server:URL"]));
            ///
            hostBuilder.Services.AddHttpClient(_webApiName, client => client.BaseAddress =
                new Uri(hostBuilder.Configuration["Server:URL"]))
                    // To avoid SSL (certificate) error.
                    .ConfigurePrimaryHttpMessageHandler(() => 
                    new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        }
                    });
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
            //var mediator = Registry.ServiceProvider.GetService<IMediator>();
            //var tokenResult = await mediator.Send(new GetToken { });
            //if (tokenResult.Status == Ardalis.Result.ResultStatus.Ok)
            //{
            //    if (tokenResult.Value == null) //// TODO: or expired
            //    {
            //        var schemesResult = await mediator.Send(new GetAuthenticationSchemes { });
            //    }
            //}


            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
