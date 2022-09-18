using Application.Features.UserSocialMediaAddresses.Dtos.DtoCommands;
using Application.Features.UserSocialMediaAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Commands.Delete
{
    public class DeleteUserSocialMediaAddressCommand :IRequest<DeletedUserSocialMediaAddressDto>,
                                                      ISecuredRequest
    {

        public int Id { get; set; }

        public string[] Roles { get; } = { "UserSocialMediaAddress.Delete" };

        public class DeleteUserSocialMediaAddressCommandHandler:IRequestHandler<DeleteUserSocialMediaAddressCommand,
                                                                                DeletedUserSocialMediaAddressDto>
        {
            private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaAddressBusinessRules _socialMediaBusinessRules;

            public DeleteUserSocialMediaAddressCommandHandler(IUserSocialMediaAddressRepository userSocialMediaAddressRepository,
                                                               IMapper mapper,
                                                               UserSocialMediaAddressBusinessRules socialMediaBusinessRules)
            {
                _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
                _mapper = mapper;
                _socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<DeletedUserSocialMediaAddressDto> Handle(DeleteUserSocialMediaAddressCommand request, CancellationToken cancellationToken)
            {
                UserSocialMediaAddress? userSocialMediaAddress = await _userSocialMediaAddressRepository.GetAsync(
                                                                u=>u.Id==request.Id
                                                                );
                await _socialMediaBusinessRules.SocialMediaAddressExists(userSocialMediaAddress);

                userSocialMediaAddress = await _userSocialMediaAddressRepository.DeleteAsync(userSocialMediaAddress);
                DeletedUserSocialMediaAddressDto mappedSocialMediaAddressDto = 
                    _mapper.Map<DeletedUserSocialMediaAddressDto>(userSocialMediaAddress);
                return mappedSocialMediaAddressDto;    


            }
        }
    }
}
