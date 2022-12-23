using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Infrastructure.Interfaces;
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
            var updatedPost = _mapper.Map<Post>(request.post);
            updatedPost.Id = request.postId;
            updatedPost.InterestPosts = new List<InterestPost>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    updatedPost.InterestPosts.Add(new InterestPost
                    {
                        InterestId = interest.Id
                    });
                }
            }
            var result = await _UOW.Posts.Update(updatedPost);
            await _UOW.Save();
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
