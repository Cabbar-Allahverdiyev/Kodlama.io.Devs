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

namespace Application.Features.ProgramingLanguages.Commands.Create
{
    public class CreateProgramingLanguageCommand : IRequest<CreatedProgramingLanguageDto>
    {
        public string Name { get; set; }
        public class CreateProgramingLanguageCommandHandler : IRequestHandler<CreateProgramingLanguageCommand,
                                                                            CreatedProgramingLanguageDto>
        {
            private readonly IProgramingLanguageRepository _programingLanguageepository;
            private readonly IMapper _mapper;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public CreateProgramingLanguageCommandHandler(IProgramingLanguageRepository programingLanguageepository, 
                                                          IMapper mapper, 
                                                          ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _programingLanguageepository = programingLanguageepository;
                _mapper = mapper;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<CreatedProgramingLanguageDto> Handle(CreateProgramingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programingLanguageBusinessRules.ProgramingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

                ProgramingLanguage mappedProgLanguage = _mapper.Map<ProgramingLanguage>(request);
                ProgramingLanguage cretedProgramingLanguage = await _programingLanguageepository.AddAsync(mappedProgLanguage);
                CreatedProgramingLanguageDto createProgramingLanguageDto = _mapper.Map<CreatedProgramingLanguageDto>(cretedProgramingLanguage);
                return createProgramingLanguageDto;

            }
        }
    }
}
