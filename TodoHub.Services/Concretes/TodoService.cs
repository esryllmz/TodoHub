using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Responses;
using TodoHub.DataAccess.Abstracts;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;
using TodoHub.Models.Entities;
using TodoHub.Services.Abstracts;
using TodoHub.Services.Rules;

namespace TodoHub.Services.Concretes
{
    public sealed class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly TodoBusinessRules _businessRules;

        public TodoService(ITodoRepository todoRepository, IMapper mapper, TodoBusinessRules businessRules)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<ReturnModel<TodoResponseDto>> Add(CreateTodoRequestDto dto, string userId)
        {
            Todo createdTodo = _mapper.Map<Todo>(dto);
            createdTodo.Id = Guid.NewGuid();
            createdTodo.UserId = userId; 

            Todo todo = _todoRepository.Add(createdTodo);

            TodoResponseDto response = _mapper.Map<TodoResponseDto>(todo);

            return new ReturnModel<TodoResponseDto>
            {
                Data = response,
                Message = "Todo eklendi.",
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<string> Delete(Guid id)
        {
            _businessRules.TodoIsPresent(id);

            Todo? todo = _todoRepository.GetById(id);
            Todo deletedTodo = _todoRepository.Delete(todo);

            return new ReturnModel<string>
            {
                Data = $"Todo Başlığı : {deletedTodo.Title}",
                Message = "Todo başarıyla silindi.",
                StatusCode = 204,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAll()
        {
            var todos = _todoRepository.GetAll();
            List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = string.Empty,
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAllByUserId(string userId)
        {
            List<Todo> todos = _todoRepository.GetAll(t => t.UserId == userId);
            List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);

            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = $"Kullanıcıya göre Todo'lar listelendi : Kullanıcı ID: {userId}",
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAllByCategoryId(int categoryId)
        {
            List<Todo> todos = _todoRepository.GetAll(t => t.CategoryId == categoryId);
            List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = $"Kategori ID'sine göre Todo'lar listelendi : Kategori ID: {categoryId}",
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAllByTitleContains(string text)
        {
            var todos = _todoRepository.GetAll(t => t.Title.Contains(text));
            var responses = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = string.Empty,
                StatusCode = 200,
                Success = true
            };
        }

        public ReturnModel<TodoResponseDto> GetById(Guid id)
        {
            try
            {
                _businessRules.TodoIsPresent(id);

                var todo = _todoRepository.GetById(id);
                var response = _mapper.Map<TodoResponseDto>(todo);
                return new ReturnModel<TodoResponseDto>
                {
                    Data = response,
                    Message = "İlgili todo gösterildi",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return GlobalExceptionHandler<TodoResponseDto>.HandleException(ex);
            }
        }

        public ReturnModel<TodoResponseDto> Update(UpdateTodoRequestDto dto)
        {
            _businessRules.TodoIsPresent(dto.Id);

            Todo todo = _todoRepository.GetById(dto.Id);

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.StartDate = dto.StartDate;
            todo.EndDate = dto.EndDate;
            todo.Priority = dto.Priority;
            todo.Completed = dto.Completed;

            _todoRepository.Update(todo);

            TodoResponseDto response = _mapper.Map<TodoResponseDto>(todo);

            return new ReturnModel<TodoResponseDto>
            {
                Data = response,
                Message = "Todo güncellendi.",
                StatusCode = 200,
                Success = true
            };
        }
    }

}
