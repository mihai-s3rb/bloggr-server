using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        private int Id { get; set; }

        public User Author { get; set; }

        public string Content { get; set; }
    }
}
