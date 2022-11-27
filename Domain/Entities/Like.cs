using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Like
    {
        private int Id { get; set; }

        public User Author { get; set; }

        public DateTime Date { get; set; }
    }
}
