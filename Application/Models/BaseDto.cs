using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models
{
    public abstract class BaseDto
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
