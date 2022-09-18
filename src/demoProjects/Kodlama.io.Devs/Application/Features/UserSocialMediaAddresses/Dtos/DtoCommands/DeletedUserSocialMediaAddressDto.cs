using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Dtos.DtoCommands
{
    public class DeletedUserSocialMediaAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GithubUrl { get; set; }
    }
}
