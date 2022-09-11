using Application.Features.UserSocialMediaAddresses.Constants.Messages;
using Application.Features.UserSocialMediaAddresses.Commands.Update;
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
            if (result.Items.Any()) throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.GithubUrlExists);
        }

        public async Task UserIdCanNotBeDuplicatedWhenInserted(int userId)
        {
            IPaginate<UserSocialMediaAddress> result = await _userSocialMediaAddressRepository.GetListAsync(
                 p => p.UserId == userId);
            if (result.Items.Any()) throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.UserExists);
        }

        public async Task UserIdCanNotBeDuplicatedWhenUpdated(UpdateUserSocialMediaAddressCommand command)
        {
            IPaginate<UserSocialMediaAddress> result = await _userSocialMediaAddressRepository.GetListAsync(
               p => p.UserId == command.Model.UserId);
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    if (item.Id != command.Id) throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.UserExists);

                }
            }
            if (result.Items.Count == 0)
                throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.UserNotExists);
        }


        public async Task GithubUrlCanNotBeDuplicatedWhenUpdated(UpdateUserSocialMediaAddressCommand command)
        {
            IPaginate<UserSocialMediaAddress> result = await _userSocialMediaAddressRepository.GetListAsync(
                p => p.GithubUrl.ToLower() == command.Model.GithubUrl.ToLower());
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    if (item.Id != command.Id)
                        throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages
                            .GithubUrlExists);

                }
            }
            if (result.Items.Count == 0)
                throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.UserNotExists);
        }

        public async Task SocialMediaAddressExists(UserSocialMediaAddress? userSocialMediaAddress)
        {
            if(userSocialMediaAddress is null)
                throw new BusinessException(UserSocialMediaAddressBusinessRuleMessages.RequestedDoesNotExists);
        }

    }
}
