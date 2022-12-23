using AutoMapper;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetById
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetPostByIdHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<PostQueryDto>? Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Posts.GetPostAllIncludedAsync(request.id);
            await _UOW.Posts.SetPostProps(result);
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
