using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUnitOfWork _UOW;

        public UpdateUserHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User result = await _UOW.Users.Update(request.user);
            await _UOW.Save();
            return result;
        }
    }
}
