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
    public partial class WebAuthenticatorPage : ContentPage
    {
        public WebAuthenticatorPage()
        {
            InitializeComponent();

            BindingContext = new WebAuthenticatorViewModel();
        }
    }
}