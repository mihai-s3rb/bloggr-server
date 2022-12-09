using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.RemoveLike
{
    public record class RemoveLikeByIdCommand(int id) : IRequest<Like>;
}
