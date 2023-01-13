using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Interfaces
{
    public interface ICustomAuthorizationHandler
    {
        public Task Authorize(int documentUserId);
    }
}
