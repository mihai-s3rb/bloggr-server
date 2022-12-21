using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggr.Application.Users.Queries.GetUsers;
using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Domain.Entities;
using Bloggr.Domain.Entities;

namespace Bloggr.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UsersQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<UsersQueryDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.user);
            User result = await _UOW.Users.Add(user);
            //make a service for this
            result.InterestUsers = new List<InterestUser>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    result.InterestUsers.Add(new InterestUser
                    {
                        InterestId = interest.Id
                    });
                }
            }
            await _UOW.Save();
            var mappedResult = _mapper.Map<UsersQueryDto>(result);
            return mappedResult;
        }
    }
}
