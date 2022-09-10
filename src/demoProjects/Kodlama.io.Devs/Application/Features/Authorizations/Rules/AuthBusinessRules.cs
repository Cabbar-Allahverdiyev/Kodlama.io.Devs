using Application.Features.Authorizations.Constants;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Rules
{
    public class AuthBusinessRules
    {
        public async Task UserShouldExistsWhenRequested(User user)
        {
            if (user == null) throw new BusinessException(AuthMessages.RequestedDoesNotExists);
        }
    }
}
