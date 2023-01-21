using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Posts.Commands.RemovePost
{
    public class RemovePostByIdHandler : IRequestHandler<RemovePostByIdCommand, PostQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly ICustomAuthorizationHandler _customAuthorizationHandler;

        public RemovePostByIdHandler(IUnitOfWork UOW, IMapper mapper, ICustomAuthorizationHandler customAuthorizationHandler)
        {
            _UOW = UOW;
            _mapper = mapper;
            _customAuthorizationHandler = customAuthorizationHandler;
        }
        public async Task<PostQueryDto> Handle(RemovePostByIdCommand request, CancellationToken cancellationToken)
        {
            var postDb = await _UOW.Posts.Query().Where(post => post.Id == request.id).FirstOrDefaultAsync();
            if (postDb == null)
                throw EntityNotFoundException.OfType<Post>();
            await _customAuthorizationHandler.Authorize(postDb.UserId);
            var result = await _UOW.Posts.RemoveById(request.id);
            await _UOW.Save();
            var mappedResult = _mapper.Map<PostQueryDto>(result);
            return mappedResult;
        }
    }
}
