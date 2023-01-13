using Bloggr.Application.Posts.Queries.GetById;
using Bloggr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Bookmarks.Commands.CreateBookmark
{
    public record class CreateBookmarkCommand(int postId) : IRequest<Bookmark>;
}
