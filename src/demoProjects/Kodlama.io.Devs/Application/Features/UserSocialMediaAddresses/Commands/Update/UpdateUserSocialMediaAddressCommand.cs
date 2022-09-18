using Application.Features.Authorizations.Rules;
using Application.Features.UserSocialMediaAddresses.Dtos.DtoCommands;
using Application.Features.UserSocialMediaAddresses.Models.Commands;
using Application.Features.UserSocialMediaAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Commands.Update
{
    public class UpdateUserSocialMediaAddressCommand : IRequest<UpdatedUserSocialMediaAddressDto>,
                                                      ISecuredRequest
    {
        public int Id { get; set; }
        public UpdateUserSocialMediaAddressModel Model { get; set; }

        public string[] Roles { get; } = { "UserSocialMediaAddress.Update" };

        public class UpdateUserSocialMediaAddressCommandHandler : IRequestHandler<UpdateUserSocialMediaAddressCommand,
                                                                                UpdatedUserSocialMediaAddressDto>
        {
            private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaAddressBusinessRules _socialMediaBusinessRules;

            public UpdateUserSocialMediaAddressCommandHandler(IUserSocialMediaAddressRepository userSocialMediaAddressRepository,
                                                               IMapper mapper,
                                                               UserSocialMediaAddressBusinessRules socialMediaBusinessRules)
            {
                _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
                _mapper = mapper;
                _socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<UpdatedUserSocialMediaAddressDto> Handle(UpdateUserSocialMediaAddressCommand request,
                                                                 CancellationToken cancellationToken)
            {
                UserSocialMediaAddress? userSocialMediaAddress = await _userSocialMediaAddressRepository.GetAsync(
                                                               u => u.Id == request.Id);

                await _socialMediaBusinessRules.SocialMediaAddressExists(userSocialMediaAddress);
                await _socialMediaBusinessRules.UserIdCanNotBeDuplicatedWhenUpdated(request);
                await _socialMediaBusinessRules.GithubUrlCanNotBeDuplicatedWhenUpdated(request);

                userSocialMediaAddress = _mapper.Map<UserSocialMediaAddress>(request.Model);
                userSocialMediaAddress.Id=request.Id;
                userSocialMediaAddress = await _userSocialMediaAddressRepository.UpdateAsync(userSocialMediaAddress);
                UpdatedUserSocialMediaAddressDto mappedSMAddressDto = _mapper.Map<UpdatedUserSocialMediaAddressDto>(userSocialMediaAddress);
                return mappedSMAddressDto;
            }
        }
    }
}
