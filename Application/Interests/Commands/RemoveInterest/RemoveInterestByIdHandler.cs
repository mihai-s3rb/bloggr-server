using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Commands.RemoveInterest
{
    public class RemoveInterestByIdHandler : IRequestHandler<RemoveInterestByIdCommand, Interest?>
    {
        private readonly IUnitOfWork _UOW;
        public RemoveInterestByIdHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<Interest?> Handle(RemoveInterestByIdCommand request, CancellationToken cancellationToken)
        {
            Interest? interest = await _UOW.Interests.RemoveById(request.id);
            return interest;
        }
    }
}
