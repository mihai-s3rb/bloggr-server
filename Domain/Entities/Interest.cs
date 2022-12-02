using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Entities
{
    public class Interest : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
