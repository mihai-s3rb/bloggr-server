using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Domain.Entities;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updatedUser = _mapper.Map<User>(request.user);
            updatedUser.Id = request.userId;
            updatedUser.InterestUsers = new List<InterestUser>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    updatedUser.InterestUsers.Add(new InterestUser
                    {
                        InterestId = interest.Id
                    });
                }
            }
            User result = await _UOW.Users.Update(updatedUser);
            await _UOW.Save();
            return result;
        }
    }
}
