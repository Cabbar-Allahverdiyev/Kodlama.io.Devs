using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Models.Commands
{
    public class CreateUserSocialMediaAddressModel
    {
        public int UserId { get; set; }
        public string GithubUrl { get; set; }
    }
}
