using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.CreateComment
{
    internal class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Comment>
    {
        private readonly IUnitOfWork _UOW;
        public CreateCommentHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var commnet = await _UOW.Comments.Add(request.comment);
            await _UOW.Save();
            return commnet;
        }
    }
}
