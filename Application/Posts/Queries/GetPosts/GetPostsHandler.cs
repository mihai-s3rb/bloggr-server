using Bloggr.Domain.Interfaces;
using Domain.Abstracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetPosts
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, IEnumerable<Post>>
    {
        private readonly IUnitOfWork _UOW;

        public GetPostsHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public async Task<IEnumerable<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {

            var query = _UOW.Posts.Query().Include(p => p.User);
            var result = await query.ToListAsync();
            return result;
        }
    }
}
