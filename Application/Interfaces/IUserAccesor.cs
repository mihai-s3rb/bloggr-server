using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interfaces
{
    public interface IUserAccessor {
        ClaimsPrincipal User { get; }

        public int GetUserId();

        public int? GetUserIdOrNull();
    }
}
