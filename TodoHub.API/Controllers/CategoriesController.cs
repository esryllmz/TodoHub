using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoHub.Models.Dtos.Category.Requests;
using TodoHub.Services.Abstracts;

namespace TodoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService _categoryService) : ControllerBase
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateCategoryRequest dto)
        {
            var result = await _categoryService.AddAsync(dto);
            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryRequest dto)
        {
            var result = await _categoryService.UpdateAsync(dto);
            return Ok(result);
        }
    }
}
