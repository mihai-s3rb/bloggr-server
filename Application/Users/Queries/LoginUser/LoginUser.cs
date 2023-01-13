using Bloggr.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.LoginUser
{
    public record class LoginUser(LoginUserDto user) : IRequest<CredentialsModel>;
}
