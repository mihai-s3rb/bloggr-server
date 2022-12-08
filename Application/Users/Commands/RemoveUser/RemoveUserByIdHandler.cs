using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.RemoveUser
{
    public class RemoveUserByIdHandler : IRequestHandler<RemoveUserByIdCommand, User>
    {
        private readonly IUnitOfWork _UOW;

        public RemoveUserByIdHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<User> Handle(RemoveUserByIdCommand request, CancellationToken cancellationToken)
        {
            User user = await _UOW.Users.RemoveById(request.id);
            await _UOW.Save();
            return user;
        }
    }
}
