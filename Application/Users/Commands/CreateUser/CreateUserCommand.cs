using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Models;
using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.CreateUser
{
    public record class CreateUserCommand(CreateUserDto user, IEnumerable<InterestQueryDto> interests) : IRequest<CredentialsModel>;
}
