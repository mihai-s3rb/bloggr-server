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
using Bloggr.Application.Models;

namespace Bloggr.Application.Comments.Commands.RemoveComment
{
    public class RemoveCommentByIdHandler : IRequestHandler<RemoveCommentByIdCommand, CommentQueryDto>
    {
        private readonly IUnitOfWork _UOW;
        private readonly ICustomAuthorizationHandler _customAuthorizationHandler;
        private readonly IMapper _mapper;

        public RemoveCommentByIdHandler(IUnitOfWork UOW, IMapper mapper, ICustomAuthorizationHandler customAuthorizationHandler)
        {
            _UOW = UOW;
            _customAuthorizationHandler = customAuthorizationHandler;
            _mapper = mapper;
        }

        public async Task<CommentQueryDto> Handle(RemoveCommentByIdCommand request, CancellationToken cancellationToken)
        {
            var commentDb = await _UOW.Comments.GetById(request.id);
            await _customAuthorizationHandler.Authorize(commentDb.UserId);
            var comment = await _UOW.Comments.RemoveById(request.id);
            await _UOW.Save();
            var result = _mapper.Map<CommentQueryDto>(comment);
            return result;
        }
    }
}
