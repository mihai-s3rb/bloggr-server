using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Posts.Queries.GetById;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.CreatePost
{
    public record class CreatePostCommand(CreatePostDto post, IEnumerable<InterestQueryDto>? interests) : IRequest<PostQueryDto>;
}
