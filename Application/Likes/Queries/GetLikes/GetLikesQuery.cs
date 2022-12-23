using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetLikes
{
    public record class GetLikesQuery : IRequest<IEnumerable<Like>>;
}
