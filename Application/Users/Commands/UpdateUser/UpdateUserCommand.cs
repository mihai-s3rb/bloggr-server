using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.UpdateUser
{
    public record class UpdateUserCommand(UpdateUserDto user, IEnumerable<InterestQueryDto>? interests, int userId) : IRequest<UsersQueryDto>;
}
