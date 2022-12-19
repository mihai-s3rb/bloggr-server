using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Comments.Queries.GetPostComments
{
    public class GetPostCommentsHandler : IRequestHandler<GetPostCommentsQuery, IEnumerable<Comment>>
    {
        private readonly IUnitOfWork _UOW;
        public GetPostCommentsHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Comment>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Comment> result = await _UOW.Comments.Find(comment => comment.PostId == request.postId);
            return result;
        }
    }
}
