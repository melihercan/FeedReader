using MobileUi.Views;
using System;
using System.Collections.Generic;

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

            BindingContext = this;
        }
    }
}
