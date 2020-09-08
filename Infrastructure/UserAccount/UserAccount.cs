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
using IdentityModel.Client;

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

        public async Task<Result<Token>> LoginAsync(User user)
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync($"{_httpClient.BaseAddress}");

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


        public Task<bool> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<string[]> GetAuthenticationSchemesAsync()
        {
            return await _httpClient.GetFromJsonAsync<string[]>("api/MobileAuth");
        }

    }
}
