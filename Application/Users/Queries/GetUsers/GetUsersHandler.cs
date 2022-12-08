using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _UOW;

        public GetUsersHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> result = await _UOW.Users.GetAll();
            return result;
        }
    }
}
