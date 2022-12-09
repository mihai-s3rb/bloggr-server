using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models.Interest
{
    public class AddInterestDTO
    {
        public string Name { get; set; }

        public int UserId { get; set; }

        public int? PostId { get; set; }
    }
}
