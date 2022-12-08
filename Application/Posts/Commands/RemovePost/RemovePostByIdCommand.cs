using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.RemovePost
{
    public record class RemovePostByIdCommand(int id) : IRequest<Post>;
}
