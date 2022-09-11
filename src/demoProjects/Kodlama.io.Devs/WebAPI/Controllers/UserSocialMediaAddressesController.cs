using Application.Features.UserSocialMediaAddresses.Commands.Create;
using Application.Features.UserSocialMediaAddresses.Commands.Delete;
using Application.Features.UserSocialMediaAddresses.Commands.Update;
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

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id,
                                                [FromBody] UpdateUserSocialMediaAddressModel model)
        {
            UpdateUserSocialMediaAddressCommand command = new() {Id=id, Model = model };
            UpdatedUserSocialMediaAddressDto result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            DeleteUserSocialMediaAddressCommand command = new() { Id = id };
            DeletedUserSocialMediaAddressDto result = await Mediator.Send(command);
            return Created("", result);
        }

    }
}
