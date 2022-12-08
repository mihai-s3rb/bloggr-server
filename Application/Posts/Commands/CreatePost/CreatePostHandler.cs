using Bloggr.Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly IUnitOfWork _UOW;
        public CreatePostHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Posts.Add(request.post);
            await _UOW.Save();
            return result;
        }
    }
}
