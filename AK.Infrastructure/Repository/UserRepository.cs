using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AK.Domain.Interfaces;
using AK.Domain.Models;
using AK.Infrastructure.Data;

namespace AK.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EFDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByIdGu(Guid id)
        {
            return await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> Register(User user)
        {
            return await base.AddAsync(user);
        }
    }
}
