using AutoMapper;
using Bloggr.Application.Likes.Queries.GetPostLikes;
using Bloggr.Infrastructure.Interfaces;
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

        public CreateLikeHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<LikeQueryDto> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var like = _mapper.Map<Like>(request.like);
            like.PostId = request.postId;
            var result = await _UOW.Likes.Add(like);
            await _UOW.Save();
            var mappedResult = _mapper.Map<LikeQueryDto>(result);
            return mappedResult;
        }
    }
}
