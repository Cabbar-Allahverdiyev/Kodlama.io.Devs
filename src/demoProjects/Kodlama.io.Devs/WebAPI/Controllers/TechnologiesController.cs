using Application.Features.Technologies.Commands.Create;
using Application.Features.Technologies.Commands.Update;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand command)
        {
            CreatedTechnologyDto result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UpdateTechnologyModel model)
        {
            UpdateTechnologyCommand command = new UpdateTechnologyCommand { Id = id, Model = model };
            UpdatedTechnologyDto result = await Mediator.Send(command);
            return Ok( result);
        }
    }
}
