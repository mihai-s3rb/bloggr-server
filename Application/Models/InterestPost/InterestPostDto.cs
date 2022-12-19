using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models.InterestPost
{
    public class InterestPostDto
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        //public Interest Interest { get; set; }

        public int InterestId { get; set; }
    }
}
