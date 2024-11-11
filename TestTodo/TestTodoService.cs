using AutoMapper;
using Moq;
using TodoHub.Core.Tokens.Services;
using TodoHub.DataAccess.Abstracts;
using TodoHub.Models.Dtos.Todo.Requests;
using TodoHub.Models.Dtos.Todo.Responses;
using TodoHub.Models.Entities;
using TodoHub.Services.Concretes;
using TodoHub.Services.Rules;

namespace TestTodo
{
    public class Tests
    {

        private Mock<ITodoRepository> _todoRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<TodoBusinessRules> _businessRulesMock;
        private Mock<DecoderService> _decoderServiceMock;
        private TodoService _todoService;

        [SetUp]
        public void Setup()
        {
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _mapperMock = new Mock<IMapper>();
            _businessRulesMock = new Mock<TodoBusinessRules>();
            _decoderServiceMock = new Mock<DecoderService>();
            _todoService = new TodoService(_todoRepositoryMock.Object, _mapperMock.Object, _businessRulesMock.Object, _decoderServiceMock.Object);
        }

        [Test]
        public async Task AddAsync_ShouldReturnSuccess_WhenTodoIsAdded()
        {
            // Arrange
            var request = new CreateTodoRequestDto
            (
              Title: "NewTodo",
              Description: "Description",
              StartDate: DateTime.Now,
              EndDate: DateTime.Now.AddDays(1),
              Priority: Priority.High,
              Completed: false,
              CategoryId: 1,
              UserId: "user123"
            );

            var createdTodo = new Todo()
            {
                Id = Guid.NewGuid(),
                Title = "NewTodo",
                Description = "Description",
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Priority = request.Priority
            };

            var responseDto = new TodoResponseDto()
            {
                Title = createdTodo.Title,
                Description = createdTodo.Description,
                StartDate = createdTodo.StartDate,
                EndDate = createdTodo.EndDate,
                Priority = createdTodo.Priority
            };

            _businessRulesMock.Setup(x => x.ValidateDates(request.StartDate, request.EndDate));
            _businessRulesMock.Setup(x => x.CheckMaxTodosPerUserAsync(request.UserId)).Returns(Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<Todo>(request)).Returns(createdTodo);
            _todoRepositoryMock.Setup(x => x.AddAsync(createdTodo)).Returns((Task<Todo>)Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<TodoResponseDto>(createdTodo)).Returns(responseDto);

            // Act
            var result = await _todoService.AddAsync(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Yapılacak iş eklendi.", result.Message);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(201, result.StatusCode);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfTodos()
        {
            // Arrange
            var todos = new List<Todo>()
            {
                new Todo()
                {
                    Id = Guid.NewGuid(),
                    Title = "FirstTodo",
                    Description = "FirstDesc",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    Priority = Priority.Normal
                },
                new Todo()
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondaryTodo",
                    Description = "SecondaryDesc",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2),
                    Priority = Priority.Low
                }
            };
            var responseDtos = new List<TodoResponseDto>()
            {
                new TodoResponseDto { Title = todos[0].Title, Description = todos[0].Description, StartDate = todos[0].StartDate, EndDate = todos[0].EndDate, Priority = todos[0].Priority },
                new TodoResponseDto { Title = todos[1].Title, Description = todos[1].Description, StartDate = todos[1].StartDate, EndDate = todos[1].EndDate, Priority = todos[1].Priority }
            };

            _todoRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(todos);
            _mapperMock.Setup(x => x.Map<List<TodoResponseDto>>(todos)).Returns(responseDtos);

            // Act
            var result = await _todoService.GetAllAsync();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Yapılacak işler listesi başarılı bir şekilde getirildi.", result.Message);
            Assert.AreEqual(responseDtos, result.Data);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnTodo_WhenTodoExists()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            var todo = new Todo()
            {
                Id = todoId,
                Title = "Todo",
                Description = "Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Priority = Priority.Normal
            };
            var responseDto = new TodoResponseDto()
            {
                Title = todo.Title,
                Description = todo.Description,
                StartDate = todo.StartDate,
                EndDate = todo.EndDate,
                Priority = todo.Priority
            };

            _businessRulesMock.Setup(x => x.IsTodoExistAsync(todoId)).Returns(Task.CompletedTask);
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync(todo);
            _mapperMock.Setup(x => x.Map<TodoResponseDto>(todo)).Returns(responseDto);

            // Act
            var result = await _todoService.GetByIdAsync(todoId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual($"{todoId} numaralı yapılacak iş başarılı bir şekilde getirildi.", result.Message);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task GetTodosByUserAsync_ShouldReturnUserSpecificTodos()
        {
            // Arrange
            var userId = "user123";
            var todos = new List<Todo>()
            {
                new Todo()
                {
                    Id = Guid.NewGuid(),
                    Title = "FirstUserTodo",
                    Description = "FirstDesc",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    UserId = userId,
                    Priority = Priority.Low
                },
                new Todo()
                {
                    Id = Guid.NewGuid(),
                    Title = "SecondaryUserTodo",
                    Description = "SecondaryDesc",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2),
                    UserId = userId,
                    Priority = Priority.Low
                }
            };
            var responseDtos = todos.Select(todo => new TodoResponseDto()
            {
                Title = todo.Title,
                Description = todo.Description,
                StartDate = todo.StartDate,
                EndDate = todo.EndDate,
                Priority = todo.Priority
            }).ToList();

            _decoderServiceMock.Setup(x => x.GetUserId()).Returns(userId);
            _todoRepositoryMock.Setup(x => x.GetByUserId(userId)).Returns(todos.AsQueryable());
            _mapperMock.Setup(x => x.Map<IQueryable<TodoResponseDto>>(todos)).Returns(responseDtos.AsQueryable());

            // Act
            var result = await _todoService.GetTodosByUserAsync();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Kullanıcıya özel todo listesi başarılı bir şekilde getirildi.", result.Message);
            Assert.AreEqual(responseDtos.AsQueryable(), result.Data);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task RemoveAsync_ShouldReturnSuccess_WhenTodoIsRemoved()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            var todo = new Todo()
            {
                Id = todoId,
                Title = "Todo",
                Description = "Description"
            };
            var deletedTodo = new Todo()
            {
                Id = todoId,
                Title = "Todo",
                Description = "Description"
            };
            var responseDto = new TodoResponseDto()
            {
                Title = todo.Title,
                Description = todo.Description
            };

            _businessRulesMock.Setup(x => x.IsTodoExistAsync(todoId)).Returns(Task.CompletedTask);
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync(todo);
            _todoRepositoryMock.Setup(x => x.RemoveAsync(todo)).ReturnsAsync(deletedTodo);
            _mapperMock.Setup(x => x.Map<TodoResponseDto>(deletedTodo)).Returns(responseDto);

            // Act
            var result = await _todoService.RemoveAsync(todoId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Yapılacak iş başarılı bir şekilde silindi", result.Message);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenTodoIsUpdated()
        {
            // Arrange
            var request = new UpdateTodoRequestDto
            (
              Id: Guid.NewGuid(),
              Title: "UpdatedTodo",
              Description: "UpdatedDescription",
              StartDate: DateTime.Now,
              EndDate: DateTime.Now.AddDays(1),
              Priority: Priority.High,
              CategoryId:1,
              Completed: false,
              UserId: "user123"
            );

            var existingTodo = new Todo()
            {
                Id = request.Id,
                Title = "OldTodo",
                Description = "OldDescription",
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Priority = Priority.Normal
            };
            var updatedTodo = new Todo()
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Priority = request.Priority
            };

            var responseDto = new TodoResponseDto()
            {
                Title = updatedTodo.Title,
                Description = updatedTodo.Description,
                StartDate = updatedTodo.StartDate,
                EndDate = updatedTodo.EndDate,
                Priority = updatedTodo.Priority
            };

            _businessRulesMock.Setup(x => x.IsTodoExistAsync(request.Id)).Returns(Task.CompletedTask);
            _todoRepositoryMock.Setup(x => x.GetByIdAsync(request.Id)).ReturnsAsync(existingTodo);
            _todoRepositoryMock.Setup(x => x.UpdateAsync(existingTodo)).ReturnsAsync(updatedTodo);
            _mapperMock.Setup(x => x.Map<TodoResponseDto>(updatedTodo)).Returns(responseDto);

            // Act
            var result = await _todoService.UpdateAsync(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Yapılacak iş başarılı bir şekilde güncellendi", result.Message);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}