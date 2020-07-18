using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared;

namespace Infrastructure
{
    public class TokenRepository : ITokenRepository
    {
        public TokenRepository()
        {
Console.WriteLine("******************************* Token Repository constructor");

            var env = Registry.ServiceProvider.GetService<IHostEnvironment>();
            var config = Registry.ServiceProvider.GetService<IConfiguration>();
            var log = Registry.ServiceProvider.GetService<ILogger<TokenRepository>>();

            log.LogInformation($"===== ENV: {env}");
            log.LogInformation($"===== CONFIG: {(config == null ? "NULL" : "OK")}");

        }

        public Task<Token> RetrieveAsync()
        {
            Console.WriteLine($"******************************* Token Repository Retrieve");
            return Task.FromResult(new Token { });
        }

        public Task StoreAsync(Token token)
        {
            return Task.CompletedTask;
        }
    }
}
