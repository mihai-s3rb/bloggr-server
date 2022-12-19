using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetPostLikes
{
    public class GetPostLikesHandler : IRequestHandler<GetPostLikesQuery, IEnumerable<Like>>
    {
        private readonly IUnitOfWork _UOW;
        public GetPostLikesHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Like>> Handle(GetPostLikesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable <Like> likesOfPost = await _UOW.Likes.Find(Like => Like.PostId == request.postId);
            return likesOfPost;
        }
    }
}
