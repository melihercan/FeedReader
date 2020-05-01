using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IIdentity
    {
        Task<bool> Authenticate(string username, string password);
    }
}
