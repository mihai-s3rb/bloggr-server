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

            var query = _UOW.Posts.Query();
            if(!string.IsNullOrEmpty(request.input))
            {
                query = query.Where(post => post.Title.Contains(request.input));
            }
            if (!string.IsNullOrEmpty(request.orderBy))
            {
                if (request.orderBy == "asc")
                    query = query.OrderBy(post => post.CreationDate);
                else if (request.orderBy == "desc")
                    query = query.OrderByDescending(post => post.CreationDate);
            }
            var test = request.interests;
            if (request.interests.Any())
            {
                query = query.Where(post => post.Interests.All(interest => request.interests.Contains(interest.Name)));
            }
            var result = await query.ToListAsync();
            return result; 
        }
    }
}
