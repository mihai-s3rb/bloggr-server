using Bloggr.Domain.Entities;
using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Interest : BaseEntity
    {
        public string Name { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public ICollection<InterestPost> InterestPosts { get; set; }

        public ICollection<InterestUser> InterestUsers { get; set; }

    }
}
