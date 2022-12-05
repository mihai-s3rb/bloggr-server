using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Commands.CreatePost
{
    public record class CreatePostCommand(Post post) : IRequest<Post>;
}
