using Bloggr.Domain.Exceptions;
using Bloggr.Domain.Models;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Services;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public async Task<IQueryable<Post>> Search(IQueryable query, string input)
        {
            var search = new SqlParameter("@Search", input);
            var searchQuery = _dbSet.FromSqlRaw(@"
                        SELECT KEY_TBL.RANK, Posts.*
                        FROM Posts
                            INNER JOIN FREETEXTTABLE(Posts,
                            (Content),
                            @Search) AS KEY_TBL
                        ON Posts.Id = KEY_TBL.[KEY]", search);
            var result = await searchQuery.ToListAsync();
            var a = "a";
            return searchQuery;
        }


        //helpers
        public async Task<Post> SetPostProps(Post entity)
        {
            entity.NumberOfLikes = await _ctx.Likes.Where(like => like.PostId == entity.Id).CountAsync();
            entity.NumberOfComments = await _ctx.Comments.Where(like => like.PostId == entity.Id).CountAsync();

            return entity;
        }

        public async Task<Post> SetPostProps(Post entity, int? userId)
        {
            entity.NumberOfLikes = await _ctx.Likes.Where(like => like.PostId == entity.Id).CountAsync();
            entity.NumberOfComments = await _ctx.Comments.Where(like => like.PostId == entity.Id).CountAsync();
            if(userId != null)
                entity.IsLikedByUser = _ctx.Likes.Where(like => like.PostId == entity.Id).Any(like => like.UserId == userId);
            //entity.IsBookmarkedByUser
            return entity;
        }

        public async Task<List<Post>> SetPostListProps(List<Post> entities, int? userId)
        {
            foreach (var entity in entities)
                await SetPostProps(entity, userId);

            return entities;
        }
    }
}
