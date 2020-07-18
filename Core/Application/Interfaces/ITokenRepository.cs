using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenRepository
    {
        Task<Token> RetrieveAsync();

        Task StoreAsync(Token token);


    }
}
