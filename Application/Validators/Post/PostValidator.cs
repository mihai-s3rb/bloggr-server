using Bloggr.Application.Posts.Commands.CreatePost;
using FluentValidation;

namespace Bloggr.Application.Validators.Post
{
    public class PostValidator : AbstractValidator<CreatePostDto>
    {
        public PostValidator()
        {
            RuleFor(p => p.Title).NotEmpty().Length(10, 400);
            RuleFor(p => p.Content).NotEmpty().Length(100, 40000);
            RuleFor(p => p.Caption).NotEmpty().Length(100, 1000);

        }
    }
}
