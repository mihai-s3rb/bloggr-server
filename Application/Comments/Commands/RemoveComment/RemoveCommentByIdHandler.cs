using Bloggr.Infrastructure.Interfaces;
using Bloggr.Application.Comments.Commands.CreateComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggr.Application.Comments.Queries.GetPostComments;
using AutoMapper;

namespace Bloggr.Application.Comments.Commands.RemoveComment
{
    public class RemoveCommentByIdHandler : IRequestHandler<RemoveCommentByIdCommand, CommentQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public RemoveCommentByIdHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<CommentQueryDto> Handle(RemoveCommentByIdCommand request, CancellationToken cancellationToken)
        {
            var comment = await _UOW.Comments.RemoveById(request.id);
            await _UOW.Save();
            var result = _mapper.Map<CommentQueryDto>(comment);
            return result;
        }
    }
}
