using Ardalis.Result;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserAccount
    {
        Task<Result<object>> RegisterAsync(User user);
        Task UnregisterAsync(User user);
        
        Task<Result<Token>> LoginAsync(User user);
        Task LogoutAsync();

        Task<string[]> GetAuthenticationSchemesAsync();

        Task<Result<Token>> ExternalLoginAsync(string scheme);


    }
}
