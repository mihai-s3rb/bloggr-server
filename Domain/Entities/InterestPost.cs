using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InterestPost : BaseEntity
    {

        public Post Post { get; set; }

        public int? PostId { get; set; }

        public Interest Interest { get; set; }

        public int InterestId { get; set; }


    }
}
