using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Queries.GetPostInterests
{
    public class GetPostInterestsHandler : IRequestHandler<GetPostInterestsQuery, IEnumerable<InterestQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetPostInterestsHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterestQueryDto>> Handle(GetPostInterestsQuery request, CancellationToken cancellationToken)
        {
            var result = await _UOW.InterestPosts.Query().Where(interestpost => interestpost.PostId == request.postId).Select(interest => interest.Interest).ToListAsync();
            var mappedResult = _mapper.Map<IEnumerable<InterestQueryDto>>(result);
            return mappedResult;
        }
    }
}
