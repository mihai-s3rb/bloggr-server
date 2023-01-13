using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models
{
    public class CredentialsModel
    {
        public string Token { get; set; }

        public UsersQueryDto User { get; set; }
    }
}
