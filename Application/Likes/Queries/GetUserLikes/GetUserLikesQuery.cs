using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetUserLikes
{
    public record class GetUserLikesQuery(int userId) : IRequest<IEnumerable<Like>>;
}
