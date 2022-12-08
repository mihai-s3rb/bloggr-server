using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, Post>
    {
        private IUnitOfWork _UOW;

        public UpdatePostHandler(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var result = await _UOW.Posts.Update(request.post);
            await _UOW.Save();
            return result;
        }
    }
}
