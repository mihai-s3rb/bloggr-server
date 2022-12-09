using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interests.Commands.CreateInterest
{
    public record class CreateInterestCommand(Interest interest) : IRequest<Interest>;
}
