using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileUi.ViewModels
{
    public class WebAuthenticatorViewModel : BaseViewModel
    {
        const string Url = "https://localhost:44392/api/mobileauth/";
////        const string Url = "https://10.0.2.2:44392/api/mobileauth/";

        public ICommand GoogleCommand { get; }
        public ICommand MicrosoftCommand { get; }

        public ICommand LocalCommand { get; }


        public WebAuthenticatorViewModel()
        {
            GoogleCommand = new Command(async () => await OnAuthenticate("Google"));
            MicrosoftCommand = new Command(async () => await OnAuthenticate("Microsoft"));
            LocalCommand = new Command(async () => await OnLocalAuthenticate());
        }

        private async Task OnLocalAuthenticate()
        {
        }

        private async Task OnAuthenticate(string scheme)
        {
            WebAuthenticatorResult result = null;

            if (scheme.Equals("Apple")
                && DeviceInfo.Platform == DevicePlatform.iOS
                && DeviceInfo.Version.Major >= 13)
            {
                result = await AppleSignInAuthenticator.AuthenticateAsync();
            }
            else
            {
                var authUrl = new Uri(Url + scheme);
                var callbackUrl = new Uri("feedreader://");

                result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
            }
        }
    }
}
