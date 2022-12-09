using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetPostComments
{
    public record class GetPostCommentsQuery(int postId) : IRequest<IEnumerable<Comment>>;
}
