using Bloggr.Application.Likes.Queries.GetPostLikes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.CreateLike
{
    public record class CreateLikeCommand(CreateLikeDto like, int postId) : IRequest<LikeQueryDto>;
}
