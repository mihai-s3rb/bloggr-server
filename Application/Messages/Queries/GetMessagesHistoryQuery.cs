using Bloggr.Application.Models;
using Bloggr.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Messages.Queries
{
    public record class GetMessagesHistoryQuery(string username, int? cursor) : IRequest<CursorPagedResult>;
}
