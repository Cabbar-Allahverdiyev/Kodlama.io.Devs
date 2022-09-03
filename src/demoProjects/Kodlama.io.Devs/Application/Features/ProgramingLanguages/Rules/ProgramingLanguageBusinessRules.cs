using Application.Features.ProgramingLanguages.Constants.Messages;
using Application.Features.ProgramingLanguages.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Rules
{
    public class ProgramingLanguageBusinessRules
    {
        private readonly IProgramingLanguageRepository _programingLanguageRepository;

        public ProgramingLanguageBusinessRules(IProgramingLanguageRepository programingLanguageRepository)
        {
            _programingLanguageRepository = programingLanguageRepository;
        }

        public async Task ProgramingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgramingLanguage> result = await _programingLanguageRepository.GetListAsync(p => p.Name.ToLower() == name.ToLower());
            if (result.Items.Any()) throw new BusinessException(ProgramingLanguageMessages.NameExists);
        }

        public async Task ProgramingLanguageNameCanNotBeDuplicatedWhenUpdated(UpdatedProgramingLanguageDto programingLanguage)
        {
            IPaginate<ProgramingLanguage> result = await _programingLanguageRepository.GetListAsync(p => p.Name.ToLower() == programingLanguage.Name.ToLower());
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    if (item.Id != programingLanguage.Id) throw new BusinessException(ProgramingLanguageMessages.NameExists);

                }
            }

        }

        public async Task ProgramingLanguageShouldExistsWhenRequested(ProgramingLanguage programingLanguage)
        {
            if (programingLanguage == null) throw new BusinessException(ProgramingLanguageMessages.RequestedDoesNotExists);
        }
    }
}
