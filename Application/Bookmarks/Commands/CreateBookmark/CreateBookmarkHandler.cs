using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloggr.Domain.Entities;

namespace Bloggr.Application.Bookmarks.Commands.CreateBookmark
{
    internal class CreateBookmarkHandler : IRequestHandler<CreateBookmarkCommand, Bookmark>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public CreateBookmarkHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<Bookmark> Handle(CreateBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = new Bookmark
            {
                UserId = _userAccessor.GetUserId(),
                PostId = request.postId
            };
            var result = _UOW.Bookmarks.Add(bookmark);
            await _UOW.Save();
            return bookmark;
        }
    }
}
