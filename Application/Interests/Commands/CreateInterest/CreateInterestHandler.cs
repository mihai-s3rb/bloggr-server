using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Commands.CreateInterest
{
    public class CreateInterestHandler : IRequestHandler<CreateInterestCommand, Interest>
    {
        private readonly IUnitOfWork _UOW;
        public CreateInterestHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<Interest> Handle(CreateInterestCommand request, CancellationToken cancellationToken)
        {
            Interest interest = await _UOW.Interests.Add(request.interest);
            await _UOW.Save();
            return interest;
        }
    }
}
