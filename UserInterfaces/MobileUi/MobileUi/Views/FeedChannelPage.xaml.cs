using MobileUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileUi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedChannelPage : ContentPage
    {
        private readonly FeedChannelViewModel _viewModel;

        public FeedChannelPage()
        {
            InitializeComponent();
            _viewModel = new FeedChannelViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnViewAppearingAsync();
        }

    }
}