using Bloggr.Application.Comments.Queries.GetPostComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.CreateComment
{
    public record class CreateCommentCommand(CreateCommentDto comment, int postId) : IRequest<CommentQueryDto>;
}
