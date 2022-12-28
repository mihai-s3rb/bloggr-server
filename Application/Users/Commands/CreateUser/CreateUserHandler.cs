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
using Microsoft.AspNetCore.Identity;
using Bloggr.Domain.Models;
using Microsoft.Data.SqlClient;
using Bloggr.Application.Interfaces;

namespace Bloggr.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthManager _authManager;

        public CreateUserHandler(IUnitOfWork UOW, IMapper mapper, UserManager<User> userManager, IAuthManager authManager)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.user);
            //User result = await _UOW.Users.Add(user);


            //make a service for this
            user.InterestUsers = new List<InterestUser>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    user.InterestUsers.Add(new InterestUser
                    {
                        InterestId = interest.Id
                    });
                }
            }
            
            var result = await _userManager.CreateAsync(user, request.user.Password);

            if (!result.Succeeded)
            {
                var errs = "";
                foreach (var error in result.Errors)
                {
                    errs += error.Description;
                }
                throw new Exception(errs);
            }

            await _userManager.AddToRoleAsync(user, "User");

            var mappedResult = _mapper.Map<UserDto>(user);
            mappedResult.Token = await _authManager.CreateToken();
            return mappedResult;
        }
    }
}
