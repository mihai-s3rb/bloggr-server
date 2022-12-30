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
using Bloggr.Domain.Exceptions;
using Bloggr.Application.Users.Queries.LoginUser;

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
                    if(_UOW.Interests.Query().Any(interestDb => interestDb.Id == interest.Id))
                    {
                        user.InterestUsers.Add(new InterestUser
                        {
                            InterestId = interest.Id
                        });
                    }
                }
            }
            
            var result = await _userManager.CreateAsync(user, request.user.Password);

            if (!result.Succeeded)
            {
                var list = new List<string>();
                foreach (var error in result.Errors)
                {
                    list.Add(error.Description);
                }
                throw new CustomException("User could've not been created", list);
            }

            await _userManager.AddToRoleAsync(user, "User");

            var mappedResult = _mapper.Map<UserDto>(user);
            var authenticatedUser = await _authManager.ValidateUser(new LoginUserDto
            {
                UserName = user.UserName,
                Password = request.user.Password
            }); ;
            if (authenticatedUser == null)
            {
                throw new CustomException("Unable to login");
            }
            mappedResult.Token = await _authManager.CreateToken();
            return mappedResult;
        }
    }
}
