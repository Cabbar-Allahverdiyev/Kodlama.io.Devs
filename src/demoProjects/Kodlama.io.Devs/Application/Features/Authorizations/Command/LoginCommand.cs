using Application.Features.Authorizations.Constants;
using Application.Features.Authorizations.Dtos;
using Application.Features.Authorizations.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Command
{
    public class LoginCommand:IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IUserRepository userRepository,
                                       IAuthService authService,
                                       AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(u=>u.Email==request.UserForLoginDto.Email);

                await _authBusinessRules.UserShouldExistsWhenRequested(user);

                if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new AuthorizationException(AuthMessages.PasswordError);
                AccessToken token = await _authService.CreateAccessToken(user);
                LoginedDto loginedDto = new() { AccessToken = token };
                return loginedDto;
               
            }
        }
    }
}
