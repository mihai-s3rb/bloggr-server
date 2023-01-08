using AutoMapper;
using Bloggr.Application.Interfaces;
using Bloggr.Domain.Entities;
using Bloggr.Domain.Exceptions;
using Bloggr.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Bookmarks.Commands.RemoveBookmark
{
    public class RemoveBookmarkHandler : IRequestHandler<RemoveBookmarkCommand, Bookmark>
    {
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public RemoveBookmarkHandler(IUnitOfWork UOW, IMapper mapper, IUserAccessor userAccessor)
        {
            _UOW = UOW;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<Bookmark> Handle(RemoveBookmarkCommand request, CancellationToken cancellationToken)
        {
            var existing = await _UOW.Bookmarks.Query().Where(bookmark => bookmark.PostId == request.postId && bookmark.UserId == _userAccessor.GetUserId()).SingleOrDefaultAsync();
            if (existing == null)
                throw EntityNotFoundException.OfType<Bookmark>();
            await _UOW.Bookmarks.Remove(existing);
            return existing;
        }
    }
}
