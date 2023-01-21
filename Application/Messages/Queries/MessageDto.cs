using Bloggr.Application.Models;
using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Messages.Queries
{
    public class MessageDto : BaseDto
    {
        public UsersQueryDto Sender { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; }
    }
}
