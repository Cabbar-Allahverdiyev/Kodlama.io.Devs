using Application.Features.UserSocialMediaAddresses.Commands.Create;
using Application.Features.UserSocialMediaAddresses.Dtos;
using Application.Features.UserSocialMediaAddresses.Models.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSocialMediaAddressesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserSocialMediaAddressModel model)
        {
            CreateUserSocialMediaAddressCommand command = new () { Model = model };
            CreatedUserSocialMediaAddressDto result = await Mediator.Send(command);
            return Created("",result);
        }
    }
}
