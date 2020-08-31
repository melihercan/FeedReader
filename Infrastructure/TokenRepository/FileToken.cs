using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TokenRepository
{
    internal class FileToken : ITokenRepository
    {
        public async Task<Token> RetrieveAsync()
        {
            await Task.CompletedTask;
            return null;
        }

        public Task StoreAsync(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
