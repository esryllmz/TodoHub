using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Responses;
using TodoHub.Core.Tokens.Services;
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
        private readonly DecoderService _decoderService;

        public TodoService(ITodoRepository todoRepository, IMapper mapper, TodoBusinessRules businessRules, DecoderService decoderService )
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _businessRules = businessRules;
            _decoderService = decoderService;
        }

        public async Task<ReturnModel<NoData>> AddAsync(CreateTodoRequestDto request)
        {
            _businessRules.ValidateDates(request.StartDate, request.EndDate);
            await _businessRules.CheckMaxTodosPerUserAsync(request.UserId);

            Todo createdTodo = _mapper.Map<Todo>(request);
            await _todoRepository.AddAsync(createdTodo);
            TodoResponseDto response = _mapper.Map<TodoResponseDto>(createdTodo);

            return new ReturnModel<NoData>()
            {
                Success = true,
                Message = "Yapılacak iş eklendi.",
                StatusCode = 200
            };
        }

        public async Task<ReturnModel<List<TodoResponseDto>>> GetAllAsync()
        {
            List<Todo> todos = await _todoRepository.GetAllAsync();
            List<TodoResponseDto> responseList = _mapper.Map<List<TodoResponseDto>>(todos);

            return new ReturnModel<List<TodoResponseDto>>()
            {
                Success = true,
                Message = "Yapılacak işler listesi başarılı bir şekilde getirildi.",
                Data = responseList,
                StatusCode = 200
            };
        }

        public async Task<ReturnModel<TodoResponseDto?>> GetByIdAsync(Guid id)
        {
            await _businessRules.IsTodoExistAsync(id);

            Todo? todo = await _todoRepository.GetByIdAsync(id);
            TodoResponseDto? response = _mapper.Map<TodoResponseDto>(todo);

            return new ReturnModel<TodoResponseDto?>()
            {
                Success = true,
                Message = $"{id} numaralı yapılacak iş başarılı bir şekilde getirildi.",
                Data = response,
                StatusCode = 200
            };
        }

        public async Task<ReturnModel<IQueryable<TodoResponseDto>>> GetTodosByUserAsync()
        {
            string userId = _decoderService.GetUserId();
            var query = _todoRepository.GetByUserId(userId);
            var todos = await query.ToListAsync();
            var responseList = _mapper.Map<IQueryable<TodoResponseDto>>(todos);

            return new ReturnModel<IQueryable<TodoResponseDto>>()
            {
                Success = true,
                Message = "Kullanıcıya özel ToDo listesi başarılı bir şekilde getirildi.",
                Data = responseList,
                StatusCode = 200
            };
        }

       

        public async Task<ReturnModel<TodoResponseDto>> RemoveAsync(Guid id)
        {
            await _businessRules.IsTodoExistAsync(id);

            Todo todo = await _todoRepository.GetByIdAsync(id);
            Todo deletedTodo = await _todoRepository.RemoveAsync(todo);
            TodoResponseDto response = _mapper.Map<TodoResponseDto>(deletedTodo);

            return new ReturnModel<TodoResponseDto>()
            {
                Success = true,
                Message = "Yapılacak iş başarılı bir şekilde silindi",
                Data = response,
                StatusCode = 200
            };
        }

        public async Task<ReturnModel<TodoResponseDto>> UpdateAsync(UpdateTodoRequestDto request)
        {
            await _businessRules.IsTodoExistAsync(request.Id);

            Todo existingTodo = await _todoRepository.GetByIdAsync(request.Id);

            existingTodo.Id = existingTodo.Id;
            existingTodo.Title = request.Title;
            existingTodo.Description = request.Description;
            existingTodo.StartDate = request.StartDate;
            existingTodo.EndDate = request.EndDate;
            existingTodo.Priority = request.Priority;
            existingTodo.Completed = request.Completed;
            existingTodo.UserId = request.UserId;

            Todo updatedTodo = await _todoRepository.UpdateAsync(existingTodo);
            TodoResponseDto dto = _mapper.Map<TodoResponseDto>(updatedTodo);

            return new ReturnModel<TodoResponseDto>()
            {
                Success = true,
                Message = "Yapılacak iş güncellendi.",
                Data = dto,
                StatusCode = 200
            };
        }
    }
}

