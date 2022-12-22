using AutoMapper;
using Bloggr.Application.Users.Queries.GetUsers;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.GetUserById
{
    public class GetUseByIdHandler : IRequestHandler<GetUserByIdQuery, UsersQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetUseByIdHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<UsersQueryDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _UOW.Users.GetUserWithInterests(request.id);
            var mappedUser = _mapper.Map<UsersQueryDto>(user);
            return mappedUser;
        }
    }
}
