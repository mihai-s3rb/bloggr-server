using Bloggr.Domain.Models;
using Bloggr.Infrastructure.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Infrastructure.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        public Task<Post> GetPostAllIncludedAsync(int id);

        public IQueryable<Post> IncludeUserAndInterests(IQueryable<Post> query);

        public Task<Post> Update(Post entity);

        public Task<Post> SetPostProps(Post entity);

        public Task<List<Post>> SetPostListProps(List<Post> entities);
    }
}
