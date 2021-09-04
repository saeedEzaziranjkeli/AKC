using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AK.Domain.Models;

namespace AK.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Register(User user);
        Task<User> GetByIdGu(Guid id);

    }
}
