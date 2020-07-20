using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared;
using TokenRepository;

namespace Infrastructure
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ILogger<TokenRepository> _logger;
        private readonly ITokenRepository _tokenRepository;

        public TokenRepository()
        {

            _logger = Registry.ServiceProvider.GetService<ILogger<TokenRepository>>();
            var configuration = Registry.ServiceProvider.GetService<IConfiguration>();
            _tokenRepository = TokenRepositoryFactory.GetTokenRepository(configuration["App:UI"]);
        }

        public async Task<Token> RetrieveAsync()
        {
            Console.WriteLine($"******************************* Token Repository Retrieve");
            return await _tokenRepository.RetrieveAsync();
        }

        public async Task StoreAsync(Token token)
        {
            await _tokenRepository.StoreAsync(token);
        }
    }
}
