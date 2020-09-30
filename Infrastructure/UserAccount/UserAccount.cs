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
using IdentityModel.OidcClient;
using InfrastructureUserAccount;
using Microsoft.Extensions.Configuration;
using IdentityModel.Client;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Infrastructure
{
    public class UserAccount : IUserAccount
    {
        private readonly ILogger<UserAccount> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public UserAccount()
        {
            _logger = Registry.ServiceProvider.GetService<ILogger<UserAccount>>();
            _configuration = Registry.ServiceProvider.GetService<IConfiguration>();
            _httpClient = Registry.ServiceProvider.GetService<HttpClient>();
        }

        public async Task RegisterAsync(User user)
        {
            await _httpClient.PostAsJsonAsync("api/UserAccount/login", user);
        }

        public Task UnregisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Token>> LoginAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/UserAccount/login", user);
            var token = await response.Content.ReadFromJsonAsync<Token>();
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token.AccessToken);

            return token;
        }

        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync("api/UserAccount/logout", null);
        }

        public async Task<string[]> GetAuthenticationSchemesAsync()
        {
            return await _httpClient.GetFromJsonAsync<string[]>("api/MobileAuth");
        }

        public async Task<Result<Token>> ExternalLoginAsync(string scheme)
        {
            Token token = null;

            try
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
                    var serverUrl = Registry.ServerUrl;
                    // For google add .nip.io (see https://nip.io/) to IP address to avoid Authorization error.
                    if (scheme.Equals("Google"))
                    {
                        var uriBuilder = new UriBuilder(serverUrl);
                        var host = uriBuilder.Host;
                        var type = Uri.CheckHostName(host);
                        if( type == UriHostNameType.IPv4)
                        {
                            uriBuilder.Host += ".nip.io";
                        }
                        serverUrl = uriBuilder.Uri.AbsoluteUri.TrimEnd('/');
                    }

                    var webAuthenticatorUrl = $"{serverUrl}/api/MobileAuth/";

                    var authUrl = new Uri(webAuthenticatorUrl + scheme);
                    var callbackUrl = new Uri("feedreader://");

                    result = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                var response = await _httpClient.PostAsJsonAsync($"api/MobileAuth/{scheme}", result.AccessToken);
                var responseToken = await response.Content.ReadFromJsonAsync<Token>();

                token = new Token
                {
                    //AccessToken = r?.AccessToken ?? r?.IdToken
                    AccessToken = responseToken.AccessToken 
                };
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
            catch (Exception ex)
            {
                return Result<Token>.Error(ex.Message);
            }

            return token;
        }
    }
}
