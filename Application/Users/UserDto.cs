using Bloggr.Application.Interests.Queries.GetInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public string UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? BackgroundImageUrl { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public IEnumerable<InterestQueryDto> Interests { get; set; }

    }
}
