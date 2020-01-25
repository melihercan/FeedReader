using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileUi.Services;
using MobileUi.Views;
using Interactors.Interfaces;

namespace MobileUi
{
    public partial class App : Application
    {

        public App(IFeedRepository feedRepository, IIdentity identity)
        {
            Interactors.Container.Init(feedRepository, identity);


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
