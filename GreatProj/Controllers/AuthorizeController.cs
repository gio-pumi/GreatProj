using GreatProj.Core.Interfaces;
using GreatProj.Core.Models;
using GreatProj.Core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace GreatProj.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        public AuthorizeController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<long>>> Register(UserRegiterDTO userRequest)
        {
            var response = await _authorizeService.Register(
                  new UserDto { UserName = userRequest.UserName }, userRequest.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto userRequest)
        {
            var response = await _authorizeService.login(userRequest.UserName, userRequest.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
