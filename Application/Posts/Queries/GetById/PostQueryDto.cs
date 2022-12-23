using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetById
{
    public class PostQueryDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Caption { get; set; }

        public int NumberOfLikes { get; set; }

        public int NumberOfComments { get; set; }

        public int? UserId { get; set; }

        public UsersQueryDto User { get; set; }

        public IEnumerable<InterestQueryDto> Interests { get; set; }
    }
}
