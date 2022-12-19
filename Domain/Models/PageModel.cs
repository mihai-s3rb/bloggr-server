using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Models
{
    public class PageModel
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
