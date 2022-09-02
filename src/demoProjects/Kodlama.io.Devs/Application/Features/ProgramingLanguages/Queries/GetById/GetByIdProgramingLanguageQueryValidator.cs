using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Queries.GetById
{
    public class GetByIdProgramingLanguageQueryValidator : AbstractValidator<GetByIdProgramingLanguageQuery>
    {
        public GetByIdProgramingLanguageQueryValidator()
        {
            RuleFor(q=>q.Id).NotEmpty();
        }
    }
}
