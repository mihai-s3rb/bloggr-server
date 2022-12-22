using AutoMapper;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetPostLikes
{
    public class GetPostLikesHandler : IRequestHandler<GetPostLikesQuery, IEnumerable<LikeQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetPostLikesHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LikeQueryDto>> Handle(GetPostLikesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Like> likesOfPost = await _UOW.Likes.Query().Include(like => like.User).Where(Like => Like.PostId == request.postId).ToListAsync();
            var result = _mapper.Map<IEnumerable<LikeQueryDto>>(likesOfPost);
            return result;
        }
    }
}
