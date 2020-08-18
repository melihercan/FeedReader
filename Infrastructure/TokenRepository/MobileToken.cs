using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TokenRepository
{
    internal class MobileToken : ITokenRepository
    {
        public async Task<Token> RetrieveAsync()
        {
            //return new Token();
            await Task.CompletedTask;
            return null;
        }

        public async Task StoreAsync(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
