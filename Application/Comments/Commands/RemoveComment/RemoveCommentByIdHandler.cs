using Bloggr.Infrastructure.Interfaces;
using Bloggr.Application.Comments.Commands.CreateComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggr.Application.Comments.Queries.GetPostComments;
using AutoMapper;
using Bloggr.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Bloggr.Domain.Exceptions;

namespace Bloggr.Application.Comments.Commands.RemoveComment
{
    public class RemoveCommentByIdHandler : IRequestHandler<RemoveCommentByIdCommand, CommentQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserAccessor _userAccessor;

        public RemoveCommentByIdHandler(IUnitOfWork UOW, IMapper mapper, IAuthorizationService authorizationService, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userAccessor = userAccessor;
        }

        public async Task<CommentQueryDto> Handle(RemoveCommentByIdCommand request, CancellationToken cancellationToken)
        {
            var commentDb = await _UOW.Comments.GetById(request.id);

            var authorizationResult = await _authorizationService
            .AuthorizeAsync(_userAccessor.User, commentDb, "EditPolicy");
            if (!authorizationResult.Succeeded)
            {
               throw new NotAuthorizedException("You are not allowed");
            }
            var comment = await _UOW.Comments.RemoveById(request.id);
            await _UOW.Save();
            var result = _mapper.Map<CommentQueryDto>(comment);
            return result;
        }
    }
}
