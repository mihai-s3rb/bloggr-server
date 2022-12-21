using AutoMapper;
using Bloggr.Application.Comments.Queries.GetPostComments;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Commands.CreateComment
{
    internal class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public CreateCommentHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<CommentQueryDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request.comment);
            comment.PostId = request.postId;
            var result = await _UOW.Comments.Add(comment);
            await _UOW.Save();
            var mappedResult = _mapper.Map<CommentQueryDto>(result);
            return mappedResult;
        }
    }
}
