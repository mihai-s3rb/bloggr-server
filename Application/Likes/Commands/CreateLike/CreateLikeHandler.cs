using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Likes.Queries.GetPostLikes;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.CreateLike
{
    public class CreateLikeHandler : IRequestHandler<CreateLikeCommand, LikeQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public CreateLikeHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<LikeQueryDto> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();

            var existing = await _UOW.Likes.Query().Where(like => like.UserId == userId && like.PostId == request.postId).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new Exception("Already liked");
            }
            var like = new Like
            {
                PostId = request.postId,
                UserId = userId
            };
            var result = await _UOW.Likes.Add(like);
            await _UOW.Save();
            var mappedResult = _mapper.Map<LikeQueryDto>(result);
            return mappedResult;
        }
    }
}
