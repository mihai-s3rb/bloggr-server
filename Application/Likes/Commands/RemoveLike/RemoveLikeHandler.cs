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

namespace Bloggr.Application.Likes.Commands.RemoveLike
{
    internal class RemoveLikeHandler : IRequestHandler<RemoveLikeCommand, LikeQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public RemoveLikeHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<LikeQueryDto> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.GetUserId();
            var like = await _UOW.Likes.Query().Where(like => like.UserId == userId && like.PostId == request.postId).FirstOrDefaultAsync();
            await _UOW.Likes.Remove(like);
            await _UOW.Save();
            var result = _mapper.Map<LikeQueryDto>(like);
            return result;
        }
    }
}
