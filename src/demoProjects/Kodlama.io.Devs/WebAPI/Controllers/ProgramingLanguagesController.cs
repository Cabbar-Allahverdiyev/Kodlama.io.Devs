using Application.Features.ProgramingLanguages.Commands.Create;
using Application.Features.ProgramingLanguages.Dtos;
using Application.Features.ProgramingLanguages.Models;
using Application.Features.ProgramingLanguages.Queries.GetById;
using Application.Features.ProgramingLanguages.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramingLanguagesController :BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgramingLanguageCommand createProgramingLanguageCommand)
        {
            CreatedProgramingLanguageDto result = await Mediator.Send(createProgramingLanguageCommand);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgramingLanguageQuery query = new() { PageRequest = pageRequest };
            ProgramingLanguageListModel model = await Mediator.Send(query);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProgramingLanguageQuery query)
        {
            ProgramingLanguageGetByIdDto result = await Mediator.Send(query);
            return Ok(result);
        }

    }
}
