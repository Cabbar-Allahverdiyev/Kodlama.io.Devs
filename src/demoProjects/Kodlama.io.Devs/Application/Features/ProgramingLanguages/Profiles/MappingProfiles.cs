using Application.Features.ProgramingLanguages.Commands.Create;
using Application.Features.ProgramingLanguages.Dtos;
using Application.Features.ProgramingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgramingLanguage, CreatedProgramingLanguageDto>().ReverseMap();
            CreateMap<ProgramingLanguage, CreateProgramingLanguageCommand>().ReverseMap();

            CreateMap<ProgramingLanguage, ProgramingLanguageGetByIdDto>().ReverseMap();

            CreateMap < IPaginate < ProgramingLanguage > ,ProgramingLanguageListModel >().ReverseMap();
            CreateMap <ProgramingLanguage, ProgramingLanguageListDto > ().ReverseMap();
            CreateMap <ProgramingLanguage, ProgramingLanguageGetByNameDto > ().ReverseMap();
            
            CreateMap <ProgramingLanguage, UpdatedProgramingLanguageDto > ().ReverseMap();

            CreateMap <ProgramingLanguage, DeletedProgramingLanguageDto > ().ReverseMap();



        }
    }
}
