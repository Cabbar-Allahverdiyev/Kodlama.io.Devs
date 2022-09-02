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

namespace Application.Features.ProgramingLanguages.Queries.GetById
{
    public class GetByIdProgramingLanguageQuery : IRequest<ProgramingLanguageGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdProgramingLanguageQueryHandler : IRequestHandler<GetByIdProgramingLanguageQuery,
                                                                            ProgramingLanguageGetByIdDto>
        {
            private readonly IProgramingLanguageRepository _programingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public GetByIdProgramingLanguageQueryHandler(IProgramingLanguageRepository programingLanguageRepository,
                                                         IMapper mapper,
                                                         ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _programingLanguageRepository = programingLanguageRepository;
                _mapper = mapper;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<ProgramingLanguageGetByIdDto> Handle(GetByIdProgramingLanguageQuery request, CancellationToken cancellationToken)
            {
                ProgramingLanguage? programingLanguage = await _programingLanguageRepository.GetAsync(p => p.Id == request.Id);

                await _programingLanguageBusinessRules.BrandShouldExistsWhenRequested(programingLanguage);

                ProgramingLanguageGetByIdDto programingLanguageGetByIdDto =
                    _mapper.Map<ProgramingLanguageGetByIdDto>(programingLanguage);
                return programingLanguageGetByIdDto;
            }
        }
    }
}
