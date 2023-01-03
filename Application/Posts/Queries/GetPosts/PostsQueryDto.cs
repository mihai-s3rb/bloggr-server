using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetPosts
{
    public class PostsQueryDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CaptionImageUrl { get; set; }

        public string Caption { get; set; }

        public int Views { get; set; }

        public int? NumberOfLikes { get; set; }

        public int? NumberOfComments { get; set; }

        public bool? IsLikedByUser { get; set; }

        public int? UserId { get; set; }
        
        public UsersQueryDto User { get; set; }

        public IEnumerable<InterestQueryDto> Interests { get; set; }
    }
}
