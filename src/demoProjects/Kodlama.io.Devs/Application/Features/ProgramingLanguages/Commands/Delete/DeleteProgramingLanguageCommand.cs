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

namespace Application.Features.ProgramingLanguages.Commands.Delete
{
    public class DeleteProgramingLanguageCommand : IRequest<DeletedProgramingLanguageDto>
    {
        public int Id { get; set; }
        public class DeleteProgramingLanguageCommandHandler
            : IRequestHandler<DeleteProgramingLanguageCommand, DeletedProgramingLanguageDto>
        {
            private readonly IProgramingLanguageRepository _programingLanguageepository;
            private readonly IMapper _mapper;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public DeleteProgramingLanguageCommandHandler(IProgramingLanguageRepository programingLanguageepository,
                                                          IMapper mapper,
                                                          ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _programingLanguageepository = programingLanguageepository;
                _mapper = mapper;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<DeletedProgramingLanguageDto> Handle(DeleteProgramingLanguageCommand request,
                                                             CancellationToken cancellationToken)
            {
                ProgramingLanguage? deletedLanguage =await _programingLanguageepository.GetAsync(p=>p.Id== request.Id);
                await _programingLanguageBusinessRules.ProgramingLanguageShouldExistsWhenRequested(deletedLanguage);
                 deletedLanguage = await _programingLanguageepository.DeleteAsync(deletedLanguage);
                DeletedProgramingLanguageDto result = _mapper.Map<DeletedProgramingLanguageDto>(deletedLanguage);
                return result;
            }
        }
    }
}
