using Bloggr.Application.Models;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetPage
{
    public record class GetPostsPageQuery(PageModel pageDto) : IRequest<PagedResultDto<PostsQueryDto>>;
}
