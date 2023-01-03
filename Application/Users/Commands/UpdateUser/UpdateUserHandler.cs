using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Users.Queries.GetUsers;
using Bloggr.Domain.Entities;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UsersQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<UsersQueryDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //create new user !!!!
            var userFromDb = await _UOW.Users.Query().AsNoTracking().Where(user => user.Id == request.userId).FirstOrDefaultAsync();

            _mapper.Map<UpdateUserDto, User>(request.user, userFromDb);

            var list = new List<InterestUser>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    list.Add(new InterestUser
                    {
                        InterestId = interest.Id
                    });
                }
            }
            userFromDb.InterestUsers = list;
            await _UOW.Users.Update(userFromDb);
            await _UOW.Save();

            var updatedUser = await _UOW.Users.Query().Include(user => user.InterestUsers).ThenInclude(interestUser => interestUser.Interest).Where(user => user.Id == request.userId).FirstOrDefaultAsync();
            var mappedResult = _mapper.Map<UsersQueryDto>(updatedUser);
            return mappedResult;
        }
    }
}
