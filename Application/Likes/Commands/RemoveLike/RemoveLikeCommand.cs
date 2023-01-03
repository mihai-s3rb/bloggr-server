using Bloggr.Application.Likes.Queries.GetPostLikes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.RemoveLike
{
    public record class RemoveLikeCommand(int postId) : IRequest<LikeQueryDto>;
}
