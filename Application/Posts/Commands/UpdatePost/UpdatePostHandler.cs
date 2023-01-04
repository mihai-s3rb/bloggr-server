using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Posts.Queries.GetById;
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

        public UpdatePostHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<PostQueryDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var postFromDb = await _UOW.Posts.Query().AsNoTracking().Where(post => post.Id == request.postId).Include(post => post.InterestPosts).ThenInclude(interestpost => interestpost.Interest).FirstOrDefaultAsync();


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
