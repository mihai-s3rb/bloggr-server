using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Services;
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
        private readonly IUserAccessor _userAccessor;

        public GetPostByIdHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<PostQueryDto>? Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Posts.GetPostAllIncludedAsync(request.id);
            result.Views = result.Views + 1;
            await _UOW.Save();
            await _UOW.Posts.SetPostProps(result, _userAccessor.GetUserIdOrNull());
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
