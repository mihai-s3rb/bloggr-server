using Bloggr.Application.Posts.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.UpdatePost
{
    public record class UpdatePostCommand(UpdatePostDto post) : IRequest<PostQueryDto>;
}
