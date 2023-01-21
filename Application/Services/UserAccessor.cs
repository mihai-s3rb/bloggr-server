using Bloggr.Application.Interfaces;
using Bloggr.Domain.Exceptions;
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

        public int GetUserId()
        {
            string value = _accessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            int id;
            try
            {
                id = Int32.Parse(value);
            }
            catch (FormatException e)
            {
                throw EntityNotFoundException.OfType<User>();
            }
            return id;
        }
        public int? GetUserIdOrNull()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return GetUserId();
            }
            return null;
        }
        public string? GetUserNameOrNull()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            }
            return null;
        }
    }
}
