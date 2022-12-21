using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetPostLikes
{
    public class LikeQueryDto
    {
        public User User { get; set; }

        public Post Post { get; set; }
    }
}
