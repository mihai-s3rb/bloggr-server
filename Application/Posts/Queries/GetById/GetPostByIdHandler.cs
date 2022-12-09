using Bloggr.Application.Posts.Queries.GetPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetById
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly IUnitOfWork _UOW;

        public GetPostByIdHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public async Task<Post>? Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Posts.GetById(request.id);
            return result;
        }
    }
}
