using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Entities
{
    public class InterestUser : BaseEntity
    {
        public User User { get; set; }

        public int UserId { get; set; }

        public Interest Interest { get; set; }

        public int InterestId { get; set; }
    }
}
