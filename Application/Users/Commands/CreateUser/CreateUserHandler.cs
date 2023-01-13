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
using Bloggr.Application.Models;

namespace Bloggr.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CredentialsModel>
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

        public async Task<CredentialsModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //make the user
            var user = _mapper.Map<User>(request.user);

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
            
            //actually create it in db
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

            //add to role
            await _userManager.AddToRoleAsync(user, "User");

            //valida the user
            var authenticatedUser = await _authManager.ValidateUser(new LoginUserDto
            {
                UserName = user.UserName,
                Password = request.user.Password
            }); ;
            if (authenticatedUser == null)
            {
                throw new CustomException("Unable to login");
            }
            //send the token and user back

            var mappedResult = new CredentialsModel
            {
                Token = await _authManager.CreateToken(),
                User = _mapper.Map<UsersQueryDto>(authenticatedUser)
            };
            return mappedResult;
        }
    }
}
