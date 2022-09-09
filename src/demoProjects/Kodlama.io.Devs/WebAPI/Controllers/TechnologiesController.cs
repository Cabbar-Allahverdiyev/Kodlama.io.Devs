using Application.Features.Technologies.Commands.Create;
using Application.Features.Technologies.Commands.Delete;
using Application.Features.Technologies.Commands.Update;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models.Commands;
using Application.Features.Technologies.Models.Queries;
using Application.Features.Technologies.Queries.GetList;
using Core.Application.Requests;
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
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteTechnologyCommand command)
        {
            DeletedTechnologyDto result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery query = new GetListTechnologyQuery { PageRequest = pageRequest };
            TechnologyListModel model = await Mediator.Send(query);
            return Ok(model);   
        }
    }
}
