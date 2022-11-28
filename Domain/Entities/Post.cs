using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Like>? Likes { get; init; }

        public ICollection<Comment>? Comments { get; init; }


    }
}
