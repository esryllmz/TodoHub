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
    Task<ReturnModel<TodoResponseDto>> Add(CreateTodoRequestDto dto, string userId);
    ReturnModel<List<TodoResponseDto>> GetAll();
    ReturnModel<TodoResponseDto> GetById(Guid id);
    ReturnModel<TodoResponseDto> Update(UpdateTodoRequestDto dto);
    ReturnModel<string> Delete(Guid id);
    ReturnModel<List<TodoResponseDto>> GetAllByCategoryId(int id);
    ReturnModel<List<TodoResponseDto>> GetAllByUserId(string userId);
    ReturnModel<List<TodoResponseDto>> GetAllByTitleContains(string text);
}
