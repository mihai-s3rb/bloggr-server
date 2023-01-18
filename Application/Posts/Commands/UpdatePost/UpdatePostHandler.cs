using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, PostQueryDto>
    {
        private IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly ICustomAuthorizationHandler _customAuthorizationHandler;

        public UpdatePostHandler(IUnitOfWork UOW, IMapper mapper, ICustomAuthorizationHandler customAuthorizationHandler)
        {
            _UOW = UOW;
            _mapper = mapper;
            _customAuthorizationHandler = customAuthorizationHandler;
        }
        public async Task<PostQueryDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var postFromDb = await _UOW.Posts.Query().AsNoTracking().Where(post => post.Id == request.postId).Include(post => post.InterestPosts).ThenInclude(interestpost => interestpost.Interest).FirstOrDefaultAsync();

            if (postFromDb == null)
                throw EntityNotFoundException.OfType<Post>();
            await _customAuthorizationHandler.Authorize(postFromDb.UserId);

            _mapper.Map<UpdatePostDto, Post>(request.post, postFromDb);

            postFromDb.InterestPosts = new List<InterestPost>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    postFromDb.InterestPosts.Add(new InterestPost
                    {
                        InterestId = interest.Id
                    });
                }
            }
            var result = await _UOW.Posts.Update(postFromDb);
            await _UOW.Save();
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
