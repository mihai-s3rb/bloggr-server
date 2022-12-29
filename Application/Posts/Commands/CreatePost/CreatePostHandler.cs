using AutoMapper;
using Bloggr.Application.Interests.Queries.GetInterests;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Domain.Entities;
using Bloggr.Infrastructure.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.CreatePost
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;

        public CreatePostHandler(IUnitOfWork UOW, IMapper mapper)
        {
            _UOW = UOW;
            _mapper = mapper;
        }
        public async Task<PostQueryDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.post);
            var result = await _UOW.Posts.Add(post);
            //make a service for this
            result.InterestPosts = new List<InterestPost>();
            if (request.interests != null && request.interests.Any())
            {
                foreach (InterestQueryDto interest in request.interests)
                {
                    if (_UOW.Interests.Query().Any(interestDb => interestDb.Id == interest.Id))
                    {
                        result.InterestPosts.Add(new InterestPost
                        {
                            InterestId = interest.Id
                        });
                    }
                }
            }
            await _UOW.Save();
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
