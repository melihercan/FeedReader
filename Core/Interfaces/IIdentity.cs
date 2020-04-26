using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IIdentity
    {
        Task<bool> Authenticate(string username, string password);
    }
}
