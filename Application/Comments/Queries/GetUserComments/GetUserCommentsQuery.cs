using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetUserComments
{
    public record class GetUserCommentsQuery(int userId) : IRequest<IEnumerable<Comment>>;
}
