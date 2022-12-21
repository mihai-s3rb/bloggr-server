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
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(BloggrContext ctx) : base(ctx)
        {
        }
        
        public async Task<Post> GetPostAllIncludedAsync(int id)
        {
            var query = _dbSet.Include(post => post.InterestPosts).ThenInclude(interestpost => interestpost.Interest).Include(post => post.User).Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id == id);
            var result = await query;
            if (result is null)
                throw EntityNotFoundException.OfType<Post>(id);
            return result;
        }

        public IQueryable<Post> IncludeUserAndInterests(IQueryable<Post> query)
        {
            return query.Include(post => post.InterestPosts).ThenInclude(interestpost => interestpost.Interest).Include(post => post.User);
        }

        public override async Task<Post> Update(Post entity)
        {
            var existing =  await _dbSet.Where(post => post.Id == entity.Id).Include(post => post.InterestPosts).ThenInclude(interestpost => interestpost.Interest).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw EntityNotFoundException.OfType<Post>();
            }
            _ctx.Entry(existing).CurrentValues.SetValues(entity);
            existing.InterestPosts = entity.InterestPosts;
            return entity;
        }
    }
}
