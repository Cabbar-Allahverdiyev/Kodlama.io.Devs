using Application.Features.Technologies.Commands.Create;
using Application.Features.Technologies.Commands.Update;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models.Commands;
using Application.Features.Technologies.Models.Queries;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateTechologyModel, Technology>().ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();

            CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();
            CreateMap<UpdateTechnologyCommand, Technology>()
                .ForMember(t => t.ProgramingLanguageId, opt => opt.MapFrom(c => c.Model.ProgramingLanguageId))
                .ForMember(t => t.Name, opt => opt.MapFrom(c => c.Model.Name))
                .ReverseMap();

            CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();


            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();

            CreateMap<Technology, TechnologyListDto>().ReverseMap();

        }
    }
}
