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
        public Task<Token> RetrieveAsync()
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
