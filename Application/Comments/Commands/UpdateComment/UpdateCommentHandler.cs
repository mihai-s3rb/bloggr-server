using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, Comment>
    {
        private readonly IUnitOfWork _UOW;
        public UpdateCommentHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<Comment> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = await _UOW.Comments.Update(request.comment);
            await _UOW.Save();
            return comment;
        }
    }
}
