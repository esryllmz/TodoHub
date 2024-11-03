using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.DataAccess.Abstracts;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;
using TodoHub.Services.Abstracts;
using TodoHub.Services.Concretes;
using TodoHub.Services.Constants;
using TodoHub.Services.Rules;



namespace Todo.Service.Tests;

public class TodoServiceTests
{
    private TodoService _todoService;
    private Mock<ITodoRepository> _mockRepository;
    private Mock<IMapper> _mockMapper;
    private Mock<TodoBusinessRules> _mockRules;

    [Setup]
    public void SetUp()
    {
        _mockRepository = new Mock<ITodoRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockRules = new Mock<TodoBusinessRules>(_mockRepository.Object);
        _todoService = new TodoService(_mockRepository.Object, _mockMapper.Object, _mockRules.Object);
    }
    [Test]
    public void GetAll_ReturnsSuccess()
    {
        // Arange
        List<Todo> posts = new List<Todo>();
        List<TodoResponseDto> responses = new();
        _mockRepository.Setup(x => x.GetAll(null, true)).Returns(posts);
        _mockMapper.Setup(x => x.Map<List<TodoResponseDto>>(posts)).Returns(responses);

        // Act 

        var result = _todoService.GetAll();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(responses, result.Data);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(string.Empty, result.Message);
    }
    [Test]
    public async Task Add_WhenTodoAdded_ReturnSuccess()
    {
        // Arrange
        CreateTodoRequestDto dto = new CreateTodoRequestDto
        {
            Title = "deneme",
            Description = "deneme",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Priority = 1
        };

        Todo todo = new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Priority = dto.Priority,
            Id = Guid.NewGuid()
        };

        TodoResponseDto response = new TodoResponseDto
        {
            Title = todo.Title,
            Description = todo.Description,
            StartDate = todo.StartDate,
            EndDate = todo.EndDate,
            Priority = todo.Priority,
            Id = todo.Id
        };

        _mockMapper.Setup(x => x.Map<Todo>(dto)).Returns(todo);
        _mockRepository.Setup(x => x.Add(todo)).Returns(todo);
        _mockMapper.Setup(x => x.Map<TodoResponseDto>(todo)).Returns(response);

        // Act 
        var result = await _todoService.Add(dto, "user-id");

        // Assert 
        Assert.IsTrue(result.Success);
        Assert.AreEqual(response, result.Data);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual("Todo eklendi.", result.Message);
    }

    [Test]
    public void GetbyId_WhenTodoIsPresent_RemoveTodo()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Todo todo = new Todo
        {
            Id = id,
            Title = "deneme",
            Description = "deneme",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Priority = 1
        };

        _mockRules.Setup(x => x.TodoIsPresent(id)).Returns(true);
        _mockRepository.Setup(x => x.GetById(id)).Returns(todo);
        _mockRepository.Setup(x => x.Delete(todo)).Returns(todo);

        // Act
        var result = _todoService.Delete(id);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual($"Todo Başlığı : {todo.Title}", result.Data);
        Assert.AreEqual("Todo başarıyla silindi.", result.Message);
    }

    [Test]
    public void GetbyId_WhenTodoIsNotPresent_RemoveFailed()
    {
        // Arrange 
        Guid id = Guid.NewGuid();

        _mockRules.Setup(x => x.TodoIsPresent(id)).Throws(new NotFoundException(Messages.TodoIsNotPresentMessage(id)));

        // Assert
        Assert.Throws<NotFoundException>(() => _todoService.Delete(id), Messages.TodoIsNotPresentMessage(id));
    }

   
}
