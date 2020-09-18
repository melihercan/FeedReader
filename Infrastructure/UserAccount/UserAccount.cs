using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Domain.Entities;
using Ardalis.Result;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Infrastructure
{
    public class UserAccount : IUserAccount
    {
        private readonly ILogger<UserAccount> _logger;
        private readonly HttpClient _httpClient;

        public UserAccount()
        {
            _logger = Registry.ServiceProvider.GetService<ILogger<UserAccount>>();
            _httpClient = Registry.ServiceProvider.GetService<HttpClient>();
        }

        public Task<Result<object>> RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UnregisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Token>> LocalLoginAsync(User user)
        {
            //var discovery = await _httpClient.GetDiscoveryDocumentAsync($"{_httpClient.BaseAddress}");

            var response = await _httpClient.PostAsJsonAsync("api/UserAccount/login", user);
            var token = await response.Content.ReadFromJsonAsync<Token>();// .ReadAsStringAsync();
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token.AccessToken);

            //try
            //{
            //    var tokenResponse = await _httpClient.RequestTokenAsync(new TokenRequest
            //    {
            //        Address = _httpClient.BaseAddress + "connect/token",
            //        ClientId = "WebUi.Client",
            //        ClientSecret = user.Password,
            //        GrantType = "client_credentials"
            //    });
            //}
            //catch(Exception ex)
            //{
            //    var x = ex.Message;
            //}

            //return null;

            return token;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string[]> GetAuthenticationSchemesAsync()
        {
            return await _httpClient.GetFromJsonAsync<string[]>("api/MobileAuth");
        }

        public async Task<Result<Token>> ExternalProviderLoginAsync(string scheme)
        {
            var authenticationUrl = $"{_httpClient.BaseAddress.AbsoluteUri}api/MobileAuth/";
            Token token = null;

            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple")
                    && DeviceInfo.Platform == DevicePlatform.iOS
                    && DeviceInfo.Version.Major >= 13)
                {
                    r = await AppleSignInAuthenticator.AuthenticateAsync();
                }
                else
                {
                    var authUrl = new Uri(authenticationUrl + scheme);
                    var callbackUrl = new Uri("feedreader://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                token = new Token
                {
                    AccessToken = r?.AccessToken ?? r?.IdToken
                };
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
            catch (Exception ex)
            {

            }

            return token;
        }



    }
}
