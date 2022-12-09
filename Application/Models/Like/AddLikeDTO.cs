using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models.Like
{
    public class AddLikeDTO
    {
        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}
