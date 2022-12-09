using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Quries.GetInterests
{
    public class GetInterestsHandler : IRequestHandler<GetInterestsQuery, IEnumerable<Interest>>
    {
        private readonly IUnitOfWork _UOW;
        public GetInterestsHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Interest>> Handle(GetInterestsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Interest> interests = await _UOW.Interests.GetAll();
            return interests;
        }
    }
}
