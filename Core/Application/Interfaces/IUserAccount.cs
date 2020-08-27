using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserAccount
    {
        Task<bool> AuthenticateAsync(string username, string password);
        Task<string[]> GetAuthenticationSchemesAsync();
    }
}
