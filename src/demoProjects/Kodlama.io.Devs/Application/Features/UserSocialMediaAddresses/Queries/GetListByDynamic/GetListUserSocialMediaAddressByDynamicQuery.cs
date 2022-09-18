using Application.Features.UserSocialMediaAddresses.Models.Queries;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Queries.GetListByDynamic
{
    public class GetListUserSocialMediaAddressByDynamicQuery : IRequest<UserSocialMediaAddressListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListUserSocialMediaAddressByDynamicQueryHandler : IRequestHandler<GetListUserSocialMediaAddressByDynamicQuery,
                                                                                        UserSocialMediaAddressListModel>
        {
            private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;
            private readonly IMapper _mapper;

            public GetListUserSocialMediaAddressByDynamicQueryHandler(IUserSocialMediaAddressRepository userSocialMediaAddressRepository, IMapper mapper)
            {
                _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
                _mapper = mapper;
            }

            public async Task<UserSocialMediaAddressListModel> Handle(GetListUserSocialMediaAddressByDynamicQuery request,
                                                                CancellationToken cancellationToken)
            {
                IPaginate<UserSocialMediaAddress> userSocialMediaAddresses =
                    await _userSocialMediaAddressRepository.GetListByDynamicAsync(
                                                                               request.Dynamic,
                                                                               include:
                                                                               m => m.Include(u => u.User),
                                                                               index: request.PageRequest.Page,
                                                                               size: request.PageRequest.PageSize);

                UserSocialMediaAddressListModel mappedModel = 
                    _mapper.Map<UserSocialMediaAddressListModel>(userSocialMediaAddresses);
                return mappedModel;
            }
        }
    }
}
