using Application.Features.Technologies.Commands.Update;
using Application.Features.Technologies.Constants.Messages;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IProgramingLanguageRepository _programingLanguageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository,
                                       IProgramingLanguageRepository programingLanguageRepository)
        {
            _technologyRepository = technologyRepository;
            _programingLanguageRepository = programingLanguageRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(
                p => p.Name.ToLower() == name.ToLower());
            if (result.Items.Any()) throw new BusinessException(TechnologyBusinessRuleMessages.NameExists);
        } 
        
        public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(UpdateTechnologyCommand command)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(p => p.Name.ToLower() == command.Model.Name.ToLower());
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    if (item.Id != command.Id) throw new BusinessException(TechnologyBusinessRuleMessages.NameExists);

                }
            }
        }

        public async Task ProgramingLanguageExists(int programingLanguageId)
        {
            ProgramingLanguage? programingLanguage = await _programingLanguageRepository
                    .GetAsync(p => p.Id == programingLanguageId);

            if (programingLanguage is null)
                throw new BusinessException(TechnologyBusinessRuleMessages.ProgramingLanguageMustBeInTheSystem);
        }

        public async Task TechnologyExists(Technology technology)
        {
           
            if (technology is null)
                throw new BusinessException(TechnologyBusinessRuleMessages.ProgramingLanguageMustBeInTheSystem);
        }


    }
}
