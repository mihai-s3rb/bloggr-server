using AutoMapper;
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
            var post = _mapper.Map<Post>(request.post);
            //get the currrent post by id

            //delete interests that don't exist in that array

            //map
            var result = await _UOW.Posts.Update(post);
            await _UOW.Save();
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
