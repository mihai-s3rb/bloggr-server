using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Queries.GetUserById
{
    public class GetUseByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUnitOfWork _UOW;

        public GetUseByIdHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await _UOW.Users.GetById(request.id);
            return user;
        }
    }
}
