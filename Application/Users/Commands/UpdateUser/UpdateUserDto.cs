using Bloggr.Application.Interests.Queries.GetInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.UpdateUser
{
    public class UpdateUserDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Bio { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public IEnumerable<InterestQueryDto> Interests { get; set; }
    }
}
