using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.GetUserById
{
    public record class GetUserByIdQuery(int id) : IRequest<UsersQueryDto>;
}
