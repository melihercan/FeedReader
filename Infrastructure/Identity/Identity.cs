using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Identity : IIdentity
    {
        public Identity()
        {
        }

        public Task<bool> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
