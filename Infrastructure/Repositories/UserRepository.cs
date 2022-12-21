using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BloggrContext ctx) : base(ctx)
        {

        }
        public override async Task<User> Update(User entity)
        {
            var existing = await _dbSet.Where(user => user.Id == entity.Id).Include(user => user.CreatedInterests).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<Post>();
            }
            _ctx.Entry(existing).CurrentValues.SetValues(entity);
            existing.CreatedInterests = entity.CreatedInterests;
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
