using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models.Comment
{
    public class AddCommentDTO
    {
        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

    }
}
