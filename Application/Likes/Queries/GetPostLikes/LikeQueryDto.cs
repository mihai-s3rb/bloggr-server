using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetPostLikes
{
    public class LikeQueryDto
    {
        public int Id { get; set; }

        public UsersQueryDto User { get; set; }

        public int PostId { get; set; }
    }
}
