using Bloggr.Application.Interests.Queries.GetInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Queries.GetUserInterests
{
    public record class GetUserInterestsCommand(int userId) : IRequest<IEnumerable<InterestQueryDto>>;
}
