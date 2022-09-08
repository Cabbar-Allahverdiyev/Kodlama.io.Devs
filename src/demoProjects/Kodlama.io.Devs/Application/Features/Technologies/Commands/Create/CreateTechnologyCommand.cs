using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models.Commands;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.Create
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public CreateTechologyModel Model { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository,
                                                  IMapper mapper, 
                                                  TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Model.Name);
                await _technologyBusinessRules.ProgramingLanguageExists(request.Model.ProgramingLanguageId);

                Technology createdTech = _mapper.Map<Technology>(request.Model);
                createdTech = await _technologyRepository.AddAsync(createdTech);
                CreatedTechnologyDto mappedCreatedTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTech);

                return mappedCreatedTechnologyDto;
            }
        }
    }
}
