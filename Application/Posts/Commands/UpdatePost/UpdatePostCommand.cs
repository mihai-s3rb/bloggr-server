using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.UpdatePost
{
    public record class UpdatePostCommand(Post post) : IRequest<Post>;
}
