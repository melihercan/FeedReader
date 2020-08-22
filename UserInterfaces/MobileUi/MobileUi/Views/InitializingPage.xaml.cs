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
    public partial class InitializingPage : ContentPage
    {
        private readonly InitializingViewModel _viewModel;

        public InitializingPage()
        {
            InitializeComponent();
            _viewModel = new InitializingViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnViewAppearingAsync();
        }
    }
}