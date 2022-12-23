using Bloggr.Application.Comments.Queries.GetPostComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.RemoveComment
{
    public record class RemoveCommentByIdCommand(int id) : IRequest<CommentQueryDto>
    {
    }
}
