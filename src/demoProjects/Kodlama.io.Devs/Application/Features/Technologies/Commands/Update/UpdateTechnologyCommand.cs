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

namespace Application.Features.Technologies.Commands.Update
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public UpdateTechnologyModel Model { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand,
                                                                      UpdatedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository,
                                                  IMapper mapper,
                                                  TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(request);
                await _technologyBusinessRules.ProgramingLanguageExists(request.Model.ProgramingLanguageId);

                Technology updatedTech = _mapper.Map<Technology>(request);
                //updatedTech.Id = request.Id;
                updatedTech = await _technologyRepository.UpdateAsync(updatedTech);
                UpdatedTechnologyDto mappedUpdateDto = _mapper.Map<UpdatedTechnologyDto>(updatedTech);
                return mappedUpdateDto;


            }
        }

    }
}
