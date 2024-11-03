using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoHub.Models.Dtos.UserResponses;
using TodoHub.Services.Abstracts;

namespace TodoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService, IAuthenticationService _authenticationService) : ControllerBase
    {


        [HttpGet("email")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var result = await _userService.GetByEmailAsync(email);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequestDto dto)
        {
            var result = await _authenticationService.RegisterAsync(dto);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authenticationService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var result = await _userService.DeleteAsync(id);
            return Ok(result);
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] string id, [FromBody] UpdateRequestDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordRequestDto dto)
        {
            var result = await _userService.ChangePasswordAsync(id, dto);
            return Ok(result);
        }

    }
}
