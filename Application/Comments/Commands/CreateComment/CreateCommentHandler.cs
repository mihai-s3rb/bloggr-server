using AutoMapper;
using Bloggr.Application.Comments.Queries.GetPostComments;
using Bloggr.Application.Interfaces;
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
        private readonly IUserAccessor _userAccessor;

        public CreateCommentHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<CommentQueryDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Content = request.comment.Content,
                PostId = request.postId,
                UserId = _userAccessor.GetUserId()
            };
            var result = await _UOW.Comments.Add(comment);
            await _UOW.Save();
            var mappedResult = _mapper.Map<CommentQueryDto>(result);
            return mappedResult;
        }
    }
}
