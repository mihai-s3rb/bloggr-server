using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Queries.GetInterests
{
    public record class GetInterestsQuery : IRequest<IEnumerable<InterestQueryDto>>;
}
