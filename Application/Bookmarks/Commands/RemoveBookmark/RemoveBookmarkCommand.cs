using Bloggr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Bookmarks.Commands.RemoveBookmark
{
    public record class RemoveBookmarkCommand(int postId) : IRequest<Bookmark>;
}
