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
        public Task<PagedResult<Post>> GetPostsWithUserAndInterestsPageAsync(PageModel pageDto);
    }
}
