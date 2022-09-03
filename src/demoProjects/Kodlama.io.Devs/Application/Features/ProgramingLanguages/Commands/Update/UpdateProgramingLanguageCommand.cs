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

namespace Application.Features.ProgramingLanguages.Commands.Update
{
    public class UpdateProgramingLanguageCommand : IRequest<UpdatedProgramingLanguageDto>
    {
        public UpdatedProgramingLanguageDto Model { get; set; }
        public class UpdateProgramingLanguageCommandHandler : IRequestHandler<UpdateProgramingLanguageCommand, UpdatedProgramingLanguageDto>
        {
            private readonly IProgramingLanguageRepository _programingLanguageepository;
            private readonly IMapper _mapper;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public UpdateProgramingLanguageCommandHandler(IProgramingLanguageRepository programingLanguageepository,
                                                          IMapper mapper,
                                                          ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _programingLanguageepository = programingLanguageepository;
                _mapper = mapper;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<UpdatedProgramingLanguageDto> Handle(UpdateProgramingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programingLanguageBusinessRules.ProgramingLanguageNameCanNotBeDuplicatedWhenUpdated(request.Model);
                
                ProgramingLanguage programingLanguage=_mapper.Map<ProgramingLanguage>(request.Model);
                programingLanguage = await _programingLanguageepository.UpdateAsync(programingLanguage);
                UpdatedProgramingLanguageDto result =
                    _mapper.Map<UpdatedProgramingLanguageDto>(programingLanguage);
                return result;
            }
        }
    }
}
