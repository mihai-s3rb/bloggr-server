using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> Update(User entity);

        public Task<User> GetUserWithInterests(int id);
    }
}
