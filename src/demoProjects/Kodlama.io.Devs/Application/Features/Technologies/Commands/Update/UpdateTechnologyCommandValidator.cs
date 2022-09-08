using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.Update
{
    public class UpdateTechnologyCommandValidator :AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Id).GreaterThan(0);

            RuleFor(c => c.Model.ProgramingLanguageId).NotEmpty();
            RuleFor(c => c.Model.ProgramingLanguageId).GreaterThan(0);

            RuleFor(c => c.Model.Name).NotEmpty();
        }
    }
}
