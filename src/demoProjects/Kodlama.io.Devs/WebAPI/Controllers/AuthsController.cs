using Application.Features.Authorizations.Command;
using Application.Features.Authorizations.Dtos;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new RegisterCommand
            {
                UserForRegisterDto = userForRegisterDto
            };

            RegisteredDto registeredDto = await Mediator.Send(registerCommand);
            return Created("", registeredDto.AccessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new LoginCommand
            {
                UserForLoginDto = userForLoginDto
            };

            LoginedDto loginedDto = await Mediator.Send(loginCommand);
            return Created("", loginedDto.AccessToken);
        }
    }
}
