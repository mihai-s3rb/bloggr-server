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

        }
    }
}
