using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Models;
using Bloggr.Infrastructure.Interfaces;
using Bloggr.Infrastructure.Services;
using Domain.Abstracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Queries.GetPosts
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, PagedResultDto<PostsQueryDto>>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetPostsHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<PagedResultDto<PostsQueryDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {

            //filtering and sorting
            var query = _UOW.Posts.Query();
            if(!string.IsNullOrEmpty(request.input))
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                var str = rgx.Replace(request.input.ToLower().Trim(), "");
                query = query.Where(post => EF.Functions.FreeText(post.Title, str) || EF.Functions.FreeText(post.Content, str));
                //query = await _UOW.Posts.Search(query, str);
            }
            if (_userAccessor.GetUserIdOrNull != null && request.isBookmarked != null && request.isBookmarked == true)
            {
                var userId = _userAccessor.GetUserId();
                query = query.Where(post => post.Bookmarks.Any(bookmark => bookmark.UserId == userId));
            }
            if (!string.IsNullOrEmpty(request.username))
            {
                query = query.Where(post => post.User.UserName == request.username);
            }
            if (request.interests != null && request.interests.Any())
            {
                query = query.Where(post => post.InterestPosts.Select(interestPost => interestPost.Interest).Any(interest => request.interests.Contains(interest.Name)) && post.InterestPosts.Count() != 0);
            }
            IOrderedQueryable<Post> orderedQuery = query.OrderByDescending(post => post.CreationDate).ThenByDescending(post => post.Views);
            if (!string.IsNullOrEmpty(request.orderBy))
            {
                if (request.orderBy == "asc")
                    orderedQuery = query.OrderBy(post => post.CreationDate);
                else if (request.orderBy == "desc")
                    orderedQuery = query.OrderByDescending(post => post.CreationDate);
                else if (request.orderBy == "pop")
                    orderedQuery = query.OrderByDescending(post => post.Views);
                else if (request.orderBy == "rec")
                    orderedQuery = query.OrderBy(post => EF.Functions.Random());
            }

            var includeQuery = orderedQuery.Include(post => post.InterestPosts).ThenInclude(interestPost => interestPost.Interest).Include(post => post.User);
            var pagedResult = await _UOW.Posts.Paginate(includeQuery, request.pageDto);

            await _UOW.Posts.SetPostListProps(pagedResult.Result, _userAccessor.GetUserIdOrNull());
            var mappedResult = PagedResultDto<PostsQueryDto>.From(pagedResult, _mapper);
            return mappedResult; 
        }
    }
}
