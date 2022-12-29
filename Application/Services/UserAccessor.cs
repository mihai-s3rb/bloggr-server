using Bloggr.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Services
{
    public class UserAccessor : IUserAccessor
    {
        private IHttpContextAccessor _accessor;
        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public ClaimsPrincipal User => _accessor.HttpContext.User;
    }
}
