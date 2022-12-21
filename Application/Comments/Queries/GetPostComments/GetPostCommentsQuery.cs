using Bloggr.Application.Models;
using Bloggr.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetPostComments
{
    public record class GetPostCommentsQuery(PageModel pageDto, int postId) : IRequest<PagedResultDto<CommentQueryDto>>;
}
