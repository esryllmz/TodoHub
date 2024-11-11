
using AutoMapper;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;
using TodoHub.Models.Entities;

namespace TodoHub.Services.Profiles;

public class TodoProfiles : Profile
{
    public TodoProfiles()
    {
        CreateMap<CreateTodoRequestDto, Todo>();
        CreateMap<UpdateTodoRequestDto, Todo>();
        CreateMap<Todo, TodoResponseDto>()
        .ForMember(t => t.Category, opt => opt.MapFrom(t => t.Category.Name))
        .ForMember(t => t.UserName, opt => opt.MapFrom(t => t.User.UserName));
    }
}
