using Application.Features.UserSocialMediaAddresses.Constants.Messages;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Rules
{
    public class UserSocialMediaAddressBusinessRules
    {
        private readonly IUserSocialMediaAddressRepository _userSocialMediaAddressRepository;

        public UserSocialMediaAddressBusinessRules(IUserSocialMediaAddressRepository userSocialMediaAddressRepository)
        {
            _userSocialMediaAddressRepository = userSocialMediaAddressRepository;
        }

        public async Task GithubUrlCanNotBeDuplicatedWhenInserted(string githubUrl)
        {
            IPaginate<UserSocialMediaAddress> result = await _userSocialMediaAddressRepository.GetListAsync(
                p => p.GithubUrl.ToLower() == githubUrl.ToLower());
            if (result.Items.Any()) throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.GithuburlExists);
        }

        public async Task UserIdCanNotBeDuplicatedWhenInserted(int userId)
        {
            IPaginate<UserSocialMediaAddress> result = await _userSocialMediaAddressRepository.GetListAsync(
                 p => p.UserId == userId);
            if (result.Items.Any()) throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.UserExists);
        }
    }
}
