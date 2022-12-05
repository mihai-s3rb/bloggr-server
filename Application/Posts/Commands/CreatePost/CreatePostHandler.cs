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
        private readonly IBaseRepository<Post> _baseRepository;
        public CreatePostHandler(IBaseRepository<Post> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            Post test = new Post
            {
                Title = "Bla bl bla",
                Content = "Ha ha ha"
            };
            return _baseRepository.Add(request.post);
        }
    }
}
