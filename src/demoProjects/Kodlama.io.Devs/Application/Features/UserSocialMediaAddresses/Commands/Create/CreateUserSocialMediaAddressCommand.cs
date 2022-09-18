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

namespace Application.Features.UserSocialMediaAddresses.Commands.Create
{
    public class CreateUserSocialMediaAddressCommand :IRequest<CreatedUserSocialMediaAddressDto>
                                                      ,ISecuredRequest
    {
        public CreateUserSocialMediaAddressModel Model { get; set; }

        public string[] Roles { get; } = { "UserSocialMediaAddress.Add" };

        public class CreateUserSocialMediaAddressCommandHandler :IRequestHandler<CreateUserSocialMediaAddressCommand,
                                                                                 CreatedUserSocialMediaAddressDto>
        {
            private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaAddressBusinessRules _socialMediaBusinessRules;
            private readonly AuthBusinessRules _authBusinessRules;

            public CreateUserSocialMediaAddressCommandHandler(IUserSocialMediaAddressRepository userSocialMediaAddressRepository,
                                                               IMapper mapper,
                                                               UserSocialMediaAddressBusinessRules socialMediaBusinessRules,
                                                               AuthBusinessRules authBusinessRules,
                                                               IUserRepository userRepository)
            {
                _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
                _userRepository = userRepository;
                _mapper = mapper;
                _socialMediaBusinessRules = socialMediaBusinessRules;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<CreatedUserSocialMediaAddressDto> Handle(CreateUserSocialMediaAddressCommand request, CancellationToken cancellationToken)
            {
                User? user =await _userRepository.GetAsync(u => u.Id == request.Model.UserId);

                await  _authBusinessRules.UserShouldExistsWhenRequested(user);

                await _socialMediaBusinessRules.GithubUrlCanNotBeDuplicatedWhenInserted(request.Model.GithubUrl);
                await _socialMediaBusinessRules.UserIdCanNotBeDuplicatedWhenInserted(request.Model.UserId);

                UserSocialMediaAddress userSocialMediaAddress = _mapper.Map<UserSocialMediaAddress>(request.Model);
                userSocialMediaAddress = await _userSocialMediaAddressRepository.AddAsync(userSocialMediaAddress) ;
                CreatedUserSocialMediaAddressDto mappedSMAddressDto = _mapper.Map<CreatedUserSocialMediaAddressDto>(userSocialMediaAddress);
                return mappedSMAddressDto;

            }
        }

       


    }
}
