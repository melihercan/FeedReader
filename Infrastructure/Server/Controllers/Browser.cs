using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;
using Xamarin.Essentials;

namespace Infrastructure.Server.Controllers
{
    public class Browser : IBrowser
    {
        private readonly string _redirectUrl;

        public Browser(string redirectUrl)
        {
            _redirectUrl = redirectUrl;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, 
            CancellationToken cancellationToken = default)
        {
            WebAuthenticatorResult authResult = await WebAuthenticator.AuthenticateAsync(
                new Uri(options.StartUrl), new Uri(_redirectUrl));
            return new BrowserResult()
            {
                Response = ParseAuthenticatorResult(authResult)
            };
        }

        string ParseAuthenticatorResult(WebAuthenticatorResult result)
        {
            string code = result?.Properties["code"];
            string scope = result?.Properties["scope"];
            string state = result?.Properties["state"];
            string sessionState = result?.Properties["session_state"];
            return $"{_redirectUrl}#code={code}&scope={scope}&state={state}&session_state={sessionState}";
        }
    }
}
