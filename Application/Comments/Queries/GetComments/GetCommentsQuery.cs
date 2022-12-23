using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetComments
{
    public record class GetCommentsQuery : IRequest<IEnumerable<Comment>>
    {
    }
}
