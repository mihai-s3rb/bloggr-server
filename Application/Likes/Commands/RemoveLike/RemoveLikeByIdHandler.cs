using AutoMapper;
using Bloggr.Application.Likes.Queries.GetPostLikes;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.RemoveLike
{
    internal class RemoveLikeByIdHandler : IRequestHandler<RemoveLikeByIdCommand, LikeQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public RemoveLikeByIdHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<LikeQueryDto> Handle(RemoveLikeByIdCommand request, CancellationToken cancellationToken)
        {
            Like? like = await _UOW.Likes.RemoveById(request.id);
            await _UOW.Save();
            var result = _mapper.Map<LikeQueryDto>(like);
            return result;
        }
    }
}
