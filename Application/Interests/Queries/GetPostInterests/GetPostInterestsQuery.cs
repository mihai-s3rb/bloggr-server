using Bloggr.Application.Interests.Queries.GetInterests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Queries.GetPostInterests
{
    public record class GetPostInterestsQuery(int postId) : IRequest<IEnumerable<InterestQueryDto>>;
}
