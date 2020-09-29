using MobileUi.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileUi
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("feedchannel", typeof(FeedChannelPage));
            Routing.RegisterRoute("about", typeof(AboutPage));

            BindingContext = this;
        }

        public ICommand LogoutCommand => new Command(async () =>
        {
        });

        public ICommand AboutCommand => new Command(async () =>
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("about");
        });

    }
}
