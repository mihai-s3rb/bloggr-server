using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models.UserModel
{
    public class AddUserDTO
    {
        public string Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public int[]? Interests { get; set; }
    }
}
