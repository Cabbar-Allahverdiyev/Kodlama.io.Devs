using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.Create
{
    public class CreateTechnologyCommandValidator :AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyCommandValidator()
        {
            RuleFor(c=>c.Model.Name).NotEmpty();

            RuleFor(c => c.Model.ProgramingLanguageId).NotEmpty();
            RuleFor(c => c.Model.ProgramingLanguageId).GreaterThan(0);
        }
    }
}
