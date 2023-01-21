using Bloggr.Application.Interfaces;
using Bloggr.Application.Models;
using Bloggr.Domain.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Services
{
    public class CustomAuthorizationHandler : ICustomAuthorizationHandler
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserAccessor _userAccessor;

        public CustomAuthorizationHandler(IAuthorizationService authorizationService, IUserAccessor userAccessor)
        {
            _authorizationService = authorizationService;
            _userAccessor = userAccessor;
        }

        public async Task Authorize(int? documentUserId)
        {
            if (documentUserId != null)
            {
                var authorizationResult = await _authorizationService
                .AuthorizeAsync(_userAccessor.User, new DocumentDto { UserId = (int)documentUserId }, "EditPolicy");
                if (!authorizationResult.Succeeded)
                {
                    throw new NotAuthorizedException("You are not allowed");
                }
            }
        }
    }
}
