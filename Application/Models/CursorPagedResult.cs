using Bloggr.Application.Messages.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Models
{
    public class CursorPagedResult
    {
        public int? NextCursor { get; set; }

        public IEnumerable<MessageDto> Result { get; set; }

    }
}
