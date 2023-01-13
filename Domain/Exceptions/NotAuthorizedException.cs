using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Exceptions
{
    public class NotAuthorizedException : CustomException
    {
        public NotAuthorizedException(string message) : base(message)
        {

        }
    }
}
