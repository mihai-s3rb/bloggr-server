using Bloggr.Application.Users.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Application.Validators.User
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username).NotNull().NotEmpty().Length(5, 20);
            RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(u => u.FirstName).NotNull().NotEmpty().Length(5, 20);
            RuleFor(u => u.LastName).NotNull().NotEmpty().Length(5, 20);
            RuleFor(u => u.Bio).NotNull().NotEmpty().Length(5, 20);
            RuleFor(u => u.BirthDate).NotNull().NotEmpty();
        }
    }
}
