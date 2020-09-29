using MediatR;
using MobileUi.Views;
using Shared;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Application.UseCases;
using Ardalis.Result;
using System.Text.Json;

namespace MobileUi
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private readonly IMediator _mediator;

        public AppShell()
        {
            _mediator = Registry.ServiceProvider.GetService<IMediator>();

            InitializeComponent();

            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("feedchannel", typeof(FeedChannelPage));
            Routing.RegisterRoute("about", typeof(AboutPage));

            BindingContext = this;
        }

        public ICommand LogoutCommand => new Command(async () =>
        {
            var result = await _mediator.Send(new Logout());
            if (result.Status == ResultStatus.Ok)
            {
                var schemesResult = await _mediator.Send(new GetAuthenticationSchemes());
                if (schemesResult.Status == ResultStatus.Ok)
                {
                    var schemesJson = JsonSerializer.Serialize(schemesResult.Value);
                    await Shell.Current.GoToAsync($"///login?schemesjson={schemesJson}");
                }
            }
        });

        public ICommand AboutCommand => new Command(async () =>
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("about");
        });

    }
}
