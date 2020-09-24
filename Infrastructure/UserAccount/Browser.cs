﻿using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;
using Xamarin.Essentials;

namespace InfrastructureUserAccount
{
    public class Browser : IBrowser
    {
        private readonly string _webAuthenticatorUrl;

        public Browser(string webAuthenticatorUrl)
        {
            _webAuthenticatorUrl = webAuthenticatorUrl;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, 
            CancellationToken cancellationToken = default)
        {
            WebAuthenticatorResult authResult = await WebAuthenticator.AuthenticateAsync(
                new Uri(_webAuthenticatorUrl), new Uri(options.EndUrl));
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
            return $"{_webAuthenticatorUrl}#code={code}&scope={scope}&state={state}&session_state={sessionState}";
        }
    }
}
