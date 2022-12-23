using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.UpdateComment
{
    public record class UpdateCommentCommand(Comment comment) : IRequest<Comment>;
}
