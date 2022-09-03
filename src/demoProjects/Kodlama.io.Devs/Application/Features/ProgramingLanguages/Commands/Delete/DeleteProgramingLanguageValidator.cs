using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Commands.Delete
{
    public class DeleteProgramingLanguageValidator : AbstractValidator<DeleteProgramingLanguageCommand>
    {
        public DeleteProgramingLanguageValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
