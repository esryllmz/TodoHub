using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Responses;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;

namespace TodoHub.Services.Abstracts;

public interface ITodoService
{
    Task<ReturnModel<List<TodoResponseDto>>> GetAllAsync();
    Task<ReturnModel<TodoResponseDto?>> GetByIdAsync(Guid id);
    Task<ReturnModel<NoData>> AddAsync(CreateTodoRequestDto request);
    Task<ReturnModel<TodoResponseDto>> UpdateAsync(UpdateTodoRequestDto request);
    Task<ReturnModel<TodoResponseDto>> RemoveAsync(Guid id);
    Task<ReturnModel<IQueryable<TodoResponseDto>>> GetTodosByUserAsync();
}
