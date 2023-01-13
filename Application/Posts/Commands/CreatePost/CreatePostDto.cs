using Bloggr.Application.Interests.Queries.GetInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.CreatePost
{
    public class CreatePostDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Caption { get; set; }

        public string CaptionImageUrl { get; set; }

        public IEnumerable<InterestQueryDto>? Interests { get; set; }
    }
}
