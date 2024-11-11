using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Services.Abstracts;
using TodoHub.Services.Concretes;

namespace TodoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController(ITodoService _todoService) : ControllerBase
    {
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _todoService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateTodoRequestDto request)
        {
            var result = await _todoService.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid id)
        {
            var result = await _todoService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var result = await _todoService.RemoveAsync(id);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTodoRequestDto request)
        {
            var result = await _todoService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpGet("getuserstodos")]
        [Authorize]
        public async Task<IActionResult> GetTodosByUserAsync()
        {
            var result = await _todoService.GetTodosByUserAsync();
            return Ok(result);
        }
    }
}
