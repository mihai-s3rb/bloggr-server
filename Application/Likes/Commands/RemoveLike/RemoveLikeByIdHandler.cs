using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Likes.Commands.RemoveLike
{
    internal class RemoveLikeByIdHandler : IRequestHandler<RemoveLikeByIdCommand, Like?>
    {
        private readonly IUnitOfWork _UOW;
        public RemoveLikeByIdHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public async Task<Like?> Handle(RemoveLikeByIdCommand request, CancellationToken cancellationToken)
        {
            Like? like = await _UOW.Likes.RemoveById(request.id);
            await _UOW.Save();
            return like;
        }
    }
}
