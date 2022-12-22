using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Queries.GetUserInterests
{
    public class GetUserInterestsHandler : IRequestHandler<GetUserInterestsCommand, IEnumerable<InterestQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetUserInterestsHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterestQueryDto>> Handle(GetUserInterestsCommand request, CancellationToken cancellationToken)
        {
            var interests = await _UOW.Interests.Query().Where(interest => interest.UserId == request.userId).ToListAsync();
            var mappedResult = _mapper.Map<IEnumerable<InterestQueryDto>>(interests);
            return mappedResult;
        }
    }
}
