using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.CreateUser
{
    public record class CreateUserCommand(User user) : IRequest<User>;
}
