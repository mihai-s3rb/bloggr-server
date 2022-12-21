using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetPostComments
{
    public class CommentQueryDto
    {
        public string Content { get; set; }

        public User User { get; set; }
    }
}
