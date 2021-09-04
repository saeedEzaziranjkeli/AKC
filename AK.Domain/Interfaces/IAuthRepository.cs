using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AK.Domain.Models;

namespace AK.Domain.Interfaces
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
