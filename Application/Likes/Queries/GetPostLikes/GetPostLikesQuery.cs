using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetPostLikes
{
    public record class GetPostLikesQuery(int postId) : IRequest<IEnumerable<Like>>
    {
    }
}
