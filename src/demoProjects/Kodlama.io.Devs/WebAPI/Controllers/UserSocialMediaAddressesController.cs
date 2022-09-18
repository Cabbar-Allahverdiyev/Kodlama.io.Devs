using Application.Features.UserSocialMediaAddresses.Commands.Create;
using Application.Features.UserSocialMediaAddresses.Commands.Delete;
using Application.Features.UserSocialMediaAddresses.Commands.Update;
using Application.Features.UserSocialMediaAddresses.Dtos.DtoCommands;
using Application.Features.UserSocialMediaAddresses.Models.Commands;
using Application.Features.UserSocialMediaAddresses.Models.Queries;
using Application.Features.UserSocialMediaAddresses.Queries.GetList;
using Application.Features.UserSocialMediaAddresses.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
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
            CreateUserSocialMediaAddressCommand command = new() { Model = model };
            CreatedUserSocialMediaAddressDto result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,
                                                [FromBody] UpdateUserSocialMediaAddressModel model)
        {
            UpdateUserSocialMediaAddressCommand command = new() { Id = id, Model = model };
            UpdatedUserSocialMediaAddressDto result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteUserSocialMediaAddressCommand command = new() { Id = id };
            DeletedUserSocialMediaAddressDto result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserSocialMediaAddressesQuery query = new() { PageRequest = pageRequest };

            UserSocialMediaAddressListModel model = await Mediator.Send(query);
            return Ok(model);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                          [FromBody] Dynamic dynamic)
        {
            GetListUserSocialMediaAddressByDynamicQuery query = new()
            {
                PageRequest = pageRequest,
                Dynamic = dynamic
            };

            UserSocialMediaAddressListModel model = await Mediator.Send(query);
            return Ok(model);
        }

    }
}
