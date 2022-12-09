using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetUserLikes
{
    public class GetUserLikesHandler : IRequestHandler<GetUserLikesQuery, IEnumerable<Like>>
    {
        private readonly IUnitOfWork _UOW;
        public GetUserLikesHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Like>> Handle(GetUserLikesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Like> likesOfUser = await _UOW.Likes.Find(Like => Like.UserId == request.userId);
            return likesOfUser;
        }
    }
}
