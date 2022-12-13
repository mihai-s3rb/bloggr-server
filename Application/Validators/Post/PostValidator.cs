using Bloggr.Application.Models.Post;
using FluentValidation;

namespace Bloggr.Application.Validators.Posts
{
    public class PostValidator : AbstractValidator<AddPostDTO>
    {
        public PostValidator()
        {
            RuleFor(p => p.Title).NotEmpty().Length(10, 400);
            RuleFor(p => p.Content).NotEmpty().Length(100, 20000);
            RuleFor(p => p.Caption).NotEmpty().Length(100, 1000);
        }
    }
}
