using Application.Features.UserSocialMediaAddresses.Dtos.DtoCommands;
using Application.Features.UserSocialMediaAddresses.Dtos.DtoQueries;
using Application.Features.UserSocialMediaAddresses.Models.Commands;
using Application.Features.UserSocialMediaAddresses.Models.Queries;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserSocialMediaAddress, CreateUserSocialMediaAddressModel>().ReverseMap();
            CreateMap<UserSocialMediaAddress, CreatedUserSocialMediaAddressDto>()
                .ForMember(a => a.FirstName, opt => opt.MapFrom(u => u.User.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(u => u.User.LastName))
                .ReverseMap();

            CreateMap<UserSocialMediaAddress, UpdateUserSocialMediaAddressModel>().ReverseMap();
            CreateMap<UserSocialMediaAddress, UpdatedUserSocialMediaAddressDto>()
                .ForMember(a => a.FirstName, opt => opt.MapFrom(u => u.User.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(u => u.User.LastName))
                .ReverseMap();  
            
            CreateMap<UserSocialMediaAddress, DeletedUserSocialMediaAddressDto>()
                .ForMember(a => a.FirstName, opt => opt.MapFrom(u => u.User.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(u => u.User.LastName))
                .ReverseMap();

            CreateMap<UserSocialMediaAddress, DeletedUserSocialMediaAddressDto>()
            .ForMember(a => a.FirstName, opt => opt.MapFrom(u => u.User.FirstName))
            .ForMember(a => a.LastName, opt => opt.MapFrom(u => u.User.LastName))
            .ReverseMap();

            CreateMap<IPaginate<UserSocialMediaAddress>, UserSocialMediaAddressListModel>()
            .ReverseMap();

            CreateMap<UserSocialMediaAddress, UserSocialMediaAddressListDto>()
                .ForMember(a => a.FirstName, opt => opt.MapFrom(u => u.User.FirstName))
                .ForMember(a => a.LastName, opt => opt.MapFrom(u => u.User.LastName))
                .ReverseMap();

        }
    }
}
