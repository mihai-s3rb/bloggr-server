using AutoMapper;
using Bloggr.Application.Users.Queries.GetUsers;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.GetUserByUsername
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UsersQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<UsersQueryDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _UOW.Users.Query().Where(user => user.UserName == request.username).Include(user => user.InterestUsers).ThenInclude(interestUser => interestUser.Interest).FirstOrDefaultAsync();
            var mappedUser = _mapper.Map<UsersQueryDto>(user);
            return mappedUser;

        }
    }
}
