using AutoMapper;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Queries.GetInterests
{
    public class GetInterestsHandler : IRequestHandler<GetInterestsQuery, IEnumerable<InterestQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetInterestsHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InterestQueryDto>> Handle(GetInterestsQuery request, CancellationToken cancellationToken)
        {
            var interests = await _UOW.Interests.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<InterestQueryDto>>(interests);
            return mappedResult;
        }
    }
}
