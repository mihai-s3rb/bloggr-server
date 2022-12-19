using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Infrastructure.Interfaces;
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
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostsQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public GetPostsHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostsQueryDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {

            //filtering
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
            //if (request.interests.Any())
            //{
            //    query = query.Where(post => post.Interests.All(interest => request.interests.Contains(interest.Name)));
            //}
            var q = query.Include(post => post.InterestPosts).ThenInclude(ip => ip.Interest).Include(post => post.User);
            var result = await q.ToListAsync();
            var mappedResult = _mapper.Map<IEnumerable<PostsQueryDto>>(result);
            return mappedResult; 
        }
    }
}
