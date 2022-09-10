﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.UserForLoginDto.Email).NotEmpty();
            RuleFor(c => c.UserForLoginDto.Email).EmailAddress();

            RuleFor(c => c.UserForLoginDto.Password).NotEmpty();
        }
    }
}
