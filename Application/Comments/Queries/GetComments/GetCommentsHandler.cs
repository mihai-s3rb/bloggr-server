using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetComments
{
    public class GetCommentsHandler : IRequestHandler<GetCommentsQuery, IEnumerable<Comment>>
    {
        private readonly IUnitOfWork _UOW;
        public GetCommentsHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Comment>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Comment> result = await _UOW.Comments.GetAll();
            return result;
        }
    }
}
