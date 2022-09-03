using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Commands.Update
{
    public class UpdateProgramingLanguageValidator:AbstractValidator<UpdateProgramingLanguageCommand>
    {
        public UpdateProgramingLanguageValidator()
        {
            RuleFor(p => p.Model.Name).NotEmpty();
            RuleFor(p => p.Model.Id).NotEmpty();
            RuleFor(p => p.Model.Id).GreaterThan(0);
        }
    }
}
