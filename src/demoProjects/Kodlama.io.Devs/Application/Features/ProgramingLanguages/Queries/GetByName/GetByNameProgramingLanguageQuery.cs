using Application.Features.ProgramingLanguages.Dtos;
using Application.Features.ProgramingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Queries.GetByName
{
    public class GetByNameProgramingLanguageQuery : IRequest<ProgramingLanguageGetByNameDto>
    {
        public string Name { get; set; }
        
        public class GetByNameProgramingLanguageQueryHandler : IRequestHandler<GetByNameProgramingLanguageQuery,
                                                                               ProgramingLanguageGetByNameDto>
        {
            private readonly IProgramingLanguageRepository _programingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public GetByNameProgramingLanguageQueryHandler(IProgramingLanguageRepository programingLanguageRepository,
                                                         IMapper mapper,
                                                         ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _programingLanguageRepository = programingLanguageRepository;
                _mapper = mapper;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<ProgramingLanguageGetByNameDto> Handle(GetByNameProgramingLanguageQuery request, CancellationToken cancellationToken)
            {
                ProgramingLanguage? programingLanguage = await _programingLanguageRepository
                                                                .GetAsync(p => p.Name.ToLower() == request.Name.ToLower());

                await _programingLanguageBusinessRules.ProgramingLanguageShouldExistsWhenRequested(programingLanguage);

                ProgramingLanguageGetByNameDto result = _mapper.Map<ProgramingLanguageGetByNameDto>(programingLanguage);
                return result;  



            }
        }
    }
}
