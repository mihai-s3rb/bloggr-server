using AutoMapper;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UsersQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetUsersHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UsersQueryDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> result = await _UOW.Users.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<UsersQueryDto>>(result);
            return mappedResult;
        }
    }
}
