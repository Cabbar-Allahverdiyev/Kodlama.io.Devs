using Application.Features.UserSocialMediaAddresses.Models.Queries;
using Application.Features.UserSocialMediaAddresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Queries.GetList
{
    public class GetListUserSocialMediaAddressesQuery : IRequest<UserSocialMediaAddressListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListUserSocialMediaAddressesQueryHandler : IRequestHandler<GetListUserSocialMediaAddressesQuery,
                                                                                  UserSocialMediaAddressListModel>
        {

            private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;
            //private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserSocialMediaAddressBusinessRules _socialMediaBusinessRules;

            public GetListUserSocialMediaAddressesQueryHandler(IUserSocialMediaAddressRepository userSocialMediaAddressRepository,
                                                               IMapper mapper,
                                                               UserSocialMediaAddressBusinessRules socialMediaBusinessRules)
            {
                _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
                _mapper = mapper;
                _socialMediaBusinessRules = socialMediaBusinessRules;
            }

            public async Task<UserSocialMediaAddressListModel> Handle(GetListUserSocialMediaAddressesQuery request,
                                                            CancellationToken cancellationToken)
            {
                IPaginate<UserSocialMediaAddress> userSocialMediaAddresses = await _userSocialMediaAddressRepository.GetListAsync(
                                                                                include:
                                                                                m => m.Include(u => u.User),
                                                                                index: request.PageRequest.Page,
                                                                                size: request.PageRequest.PageSize);
                UserSocialMediaAddressListModel mappedModel = _mapper.Map<UserSocialMediaAddressListModel>(userSocialMediaAddresses);
                return mappedModel;
            }
        }
    }
}
