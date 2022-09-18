using Application.Features.UserSocialMediaAddresses.Dtos.DtoQueries;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserSocialMediaAddresses.Models.Queries
{
    public class UserSocialMediaAddressListModel :BasePageableModel
    {
        public IList<UserSocialMediaAddressListDto> Items { get; set; }
    }
}
