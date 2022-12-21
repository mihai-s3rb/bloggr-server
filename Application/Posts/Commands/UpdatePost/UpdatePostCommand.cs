using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Posts.Commands.CreatePost;
using Bloggr.Application.Posts.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.UpdatePost
{
    public record class UpdatePostCommand(UpdatePostDto post, IEnumerable<InterestQueryDto> interests, int postId) : IRequest<PostQueryDto>;
}
