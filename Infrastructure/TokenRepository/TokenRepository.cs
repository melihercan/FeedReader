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
        private readonly ILogger<TokenRepository> _logger;
        private readonly IConfiguration _configuration;

        public TokenRepository()
        {

            _configuration = Registry.ServiceProvider.GetService<IConfiguration>();
            _logger = Registry.ServiceProvider.GetService<ILogger<TokenRepository>>();

            var ui = _configuration["App:UI"];
            Console.WriteLine($"******************************* Token Repository constructor UI:{ui}");


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
