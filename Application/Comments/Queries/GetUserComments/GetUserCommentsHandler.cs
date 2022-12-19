using Bloggr.Infrastructure.Interfaces;
using Bloggr.Application.Comments.Queries.GetPostComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetUserComments
{
    public class GetUserCommentsHandler : IRequestHandler<GetUserCommentsQuery, IEnumerable<Comment>>
    {
        private readonly IUnitOfWork _UOW;
        public GetUserCommentsHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Comment>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Comment> result = await _UOW.Comments.Find(comment => comment.UserId == request.userId);
            return result;
        }
    }
}
