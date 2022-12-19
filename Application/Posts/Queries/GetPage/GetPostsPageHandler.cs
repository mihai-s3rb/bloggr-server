using AutoMapper;
using Bloggr.Application.Models;
using Bloggr.Application.Posts.Queries.GetPosts;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetPage
{
    public class GetPostsPageHandler : IRequestHandler<GetPostsPageQuery, PagedResultDto<PostsQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetPostsPageHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<PostsQueryDto>> Handle(GetPostsPageQuery request, CancellationToken cancellationToken)
        {
            var page = await _UOW.Posts.GetPostsWithUserAndInterestsPageAsync(request.pageDto);
            var pageModel = PagedResultDto<PostsQueryDto>.From(page, _mapper);
            return pageModel;
        }
    }
}
