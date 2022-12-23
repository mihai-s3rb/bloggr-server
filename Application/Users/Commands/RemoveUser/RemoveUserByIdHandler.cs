using AutoMapper;
using Bloggr.Application.Users.Queries.GetUsers;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.RemoveUser
{
    public class RemoveUserByIdHandler : IRequestHandler<RemoveUserByIdCommand, UsersQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public RemoveUserByIdHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<UsersQueryDto> Handle(RemoveUserByIdCommand request, CancellationToken cancellationToken)
        {
            User user = await _UOW.Users.RemoveById(request.id);
            await _UOW.Save();
            var mappedUser = _mapper.Map<UsersQueryDto>(user); 
            return mappedUser;
        }
    }
}
