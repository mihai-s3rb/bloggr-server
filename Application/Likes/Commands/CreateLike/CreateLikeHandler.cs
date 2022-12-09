using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.CreateLike
{
    public class CreateLikeHandler : IRequestHandler<CreateLikeCommand, Like>
    {
        private readonly IUnitOfWork _UOW;
        public CreateLikeHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<Like> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            Like like = await _UOW.Likes.Add(request.like);
            await _UOW.Save();
            return like;
        }
    }
}
