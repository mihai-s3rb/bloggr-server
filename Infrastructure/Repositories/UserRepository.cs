using Bloggr.Domain.Exceptions;
using Bloggr.Domain.Models;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Services;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected BloggrContext _ctx;
        protected DbSet<User> _dbSet;

        public UserRepository(BloggrContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<User>();
        }

        public async Task<User> GetById(int id)
        {
            User? result = await _dbSet.FindAsync(id);
            if (result == null)
            {
                throw EntityNotFoundException.OfType<User>();
            }
            return result;
        }
        public IQueryable<User> Query()
        {
            return _dbSet;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> results = await _dbSet.AsNoTracking().ToListAsync();
            return results;
        }
        public async Task<PagedResult<User>> Paginate(IQueryable<User> query, PageModel pageDto)
        {
            var pagedResult = await PagedResult<User>.FromAsync(query, pageDto);
            return pagedResult;
        }

        public async Task<User> Add(User entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<IEnumerable<User>> AddRange(IEnumerable<User> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        public async Task<User> Remove(User entity)
        {
            _ctx.Remove(entity);
            return entity;
        }

        public async Task<User?> RemoveById(int id)
        {
            User? existing = await _dbSet.FindAsync(id);
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<User>();
            }
            _ctx.Remove(existing);
            return existing;
        }

        public async Task<IEnumerable<User>> RemoveRange(IEnumerable<User> entities)
        {
            _ctx.RemoveRange(entities);
            return entities;
        }


        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> expression)
        {
            IEnumerable<User> results = await _dbSet.Where(expression).ToListAsync();
            return results;
        }
    public async Task<User> Update(User entity)
        {
            var existing = await _dbSet.Where(user => user.Id == entity.Id).Include(user => user.InterestUsers).ThenInclude(interestUser => interestUser.Interest).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<Post>();
            }
            _ctx.Entry(existing).CurrentValues.SetValues(entity);
            existing.InterestUsers = entity.InterestUsers;
            return entity;
        }

        public async Task<User> GetUserWithInterests(int id)
        {
            var existing = await _dbSet.Where(user => user.Id == id).Include(user => user.InterestUsers).ThenInclude(interestUser => interestUser.Interest).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<Post>();
            }
            return existing;
        }
    }
}
