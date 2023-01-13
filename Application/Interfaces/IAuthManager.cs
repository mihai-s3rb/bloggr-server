using Bloggr.Application.Users.Queries.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interfaces
{
    public interface IAuthManager
    {
        public Task<string> CreateToken();

        public Task<User?> ValidateUser(LoginUserDto userDto);
    }
}
