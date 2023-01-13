using Bloggr.Domain.Models;
using Bloggr.Infrastructure.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetById(int id);

        public IQueryable<User> Query();

        public Task<IEnumerable<User>> GetAll();

        public Task<PagedResult<User>> Paginate(IQueryable<User> query, PageModel pageDto);

        public Task<User> Add(User entity);

        public Task<IEnumerable<User>> AddRange(IEnumerable<User> entities);

        public Task<User> Remove(User entity);

        public Task<User?> RemoveById(int id);

        public Task<IEnumerable<User>> RemoveRange(IEnumerable<User> entities);

        public Task<IEnumerable<User>> Find(Expression<Func<User, bool>> expression);

        public Task<User> Update(User entity);

        public Task<User> GetUserWithInterests(int id);
    }
}
