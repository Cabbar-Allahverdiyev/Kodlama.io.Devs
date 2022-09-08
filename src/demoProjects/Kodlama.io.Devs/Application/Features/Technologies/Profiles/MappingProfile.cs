using Application.Features.Technologies.Models.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTechologyModel, Technology>().ReverseMap();
        }
    }
}
