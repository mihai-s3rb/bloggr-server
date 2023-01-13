using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Interfaces;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestHandler : IRequestHandler<CreateInterestCommand, InterestQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public CreateInterestHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<InterestQueryDto> Handle(CreateInterestCommand request, CancellationToken cancellationToken)
        {
            var interest = _mapper.Map<Interest>(request.interest);
            interest.UserId = _userAccessor.GetUserId();
            var result = await _UOW.Interests.Add(interest);
            await _UOW.Save();
            var mappedResult = _mapper.Map<InterestQueryDto>(result);
            return mappedResult;
        }
    }
}
