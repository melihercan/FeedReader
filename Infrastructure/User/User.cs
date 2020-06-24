using Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class User : IUser
    {
        public Task<bool> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
