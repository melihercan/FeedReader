using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Infrastructure
{
    public class User : IUser
    {
        private readonly ILogger<User> _logger;
        private readonly HttpClient _httpClient;

        public User()
        {
            _logger = Registry.ServiceProvider.GetService<ILogger<User>>();
            _httpClient = Registry.ServiceProvider.GetService<HttpClient>();
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
