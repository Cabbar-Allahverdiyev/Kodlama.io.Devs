using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Commands.Create
{
    public class CreateUserSocialMediaAddressValidator:AbstractValidator<CreateUserSocialMediaAddressCommand>
    {
        public CreateUserSocialMediaAddressValidator()
        {
            RuleFor(c => c.Model.UserId).NotEmpty();
            RuleFor(c => c.Model.UserId).GreaterThan(0);

            RuleFor(c => c.Model.GithubUrl).NotEmpty();

        }
    }
}
