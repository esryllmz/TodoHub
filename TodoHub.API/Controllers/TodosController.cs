using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Services.Abstracts;

namespace TodoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService _todoService) : ControllerBase
    {
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _todoService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateTodoRequestDto dto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _todoService.Add(dto, userId);
            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _todoService.GetById(id);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            var result = _todoService.Delete(id);
            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateTodoRequestDto dto)
        {
            var result = _todoService.Update(dto);
            return Ok(result);
        }

      
    }
}
