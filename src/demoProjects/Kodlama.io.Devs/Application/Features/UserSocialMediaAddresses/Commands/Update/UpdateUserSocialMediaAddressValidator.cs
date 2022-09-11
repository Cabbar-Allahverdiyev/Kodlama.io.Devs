using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Commands.Update
{
    public class UpdateUserSocialMediaAddressValidator : AbstractValidator<UpdateUserSocialMediaAddressCommand>
    {
        public UpdateUserSocialMediaAddressValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Id).GreaterThan(0);

            RuleFor(c => c.Model.UserId).NotEmpty();
            RuleFor(c => c.Model.UserId).GreaterThan(0);

            RuleFor(c => c.Model.GithubUrl).NotEmpty();

        }
    }
}
