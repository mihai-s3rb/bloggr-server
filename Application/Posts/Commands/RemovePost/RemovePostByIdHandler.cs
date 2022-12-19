using AutoMapper;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.RemovePost
{
    public class RemovePostByIdHandler : IRequestHandler<RemovePostByIdCommand, PostQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public RemovePostByIdHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<PostQueryDto> Handle(RemovePostByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Posts.RemoveById(request.id);
            await _UOW.Save();
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
