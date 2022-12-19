using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Queries.GetLikes
{
    public class GetLikesHandler : IRequestHandler<GetLikesQuery, IEnumerable<Like>>
    {
        private readonly IUnitOfWork _UOW;
        public GetLikesHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<IEnumerable<Like>> Handle(GetLikesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Like> likes = await _UOW.Likes.GetAll();
            return likes;
        }
    }
}
