using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        private int Id { get; set; }

        public User Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public List<Like>? Likes { get; set; }

        public List<Comment>? Comments { get; set; }


    }
}
