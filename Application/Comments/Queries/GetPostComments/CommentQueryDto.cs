using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetPostComments
{
    public class CommentQueryDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public UsersQueryDto User { get; set; }
    }
}
