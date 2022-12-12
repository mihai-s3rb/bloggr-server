using Bloggr.Application.Models.Post;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Validators.Posts
{
    public class PostValidator : AbstractValidator<AddPostDTO>
    {
        public PostValidator()
        {
            RuleFor(p => p.Title).NotNull().Length(10, 100);
        }
    }
}
