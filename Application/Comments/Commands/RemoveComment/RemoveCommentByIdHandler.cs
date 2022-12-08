using Bloggr.Application.Comments.Commands.CreateComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.RemoveComment
{
    public class RemoveCommentByIdHandler : IRequestHandler<RemoveCommentByIdCommand, Comment?>
    {
        private readonly IUnitOfWork _UOW;
        public RemoveCommentByIdHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        async Task<Comment?> IRequestHandler<RemoveCommentByIdCommand, Comment?>.Handle(RemoveCommentByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Comments.RemoveById(request.id);
            await _UOW.Save();
            return result;
        }
    }
}
