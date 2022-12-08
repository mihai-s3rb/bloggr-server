using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.RemovePost
{
    public record class RemoveByIdCommand(int id) : IRequest<Post>;
}
