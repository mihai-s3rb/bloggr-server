using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bloggr.Application.Users.Queries.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUser, UserDto>
    {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;

        public LoginUserHandler(IAuthManager authManager, IMapper mapper)
        {
            _authManager = authManager;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var authenticatedUser = await _authManager.ValidateUser(request.user);
            if (authenticatedUser == null)
            {
                throw new Exception("Unauthorized");
            }

            var mappedUser = _mapper.Map<UserDto>(authenticatedUser);
            mappedUser.Token = await _authManager.CreateToken();
            return mappedUser;
        }
    }
}
