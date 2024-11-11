using AutoMapper;
using Moq;

namespace Todo.Test
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
        public async Task AddAsync_ShouldReturnSuccess_WhenToDoIsAdded()
        {
            // Arrange
            var request = new CreateTodoRequestDto
            (
                Title: "NewTodo",
                Description: "Description",
                StartDate: DateTime.Now,
                EndDate: DateTime.Now.AddDays(1),
                Priority: Priority.Low,
                CategoryId: 1,
                UserId: "user123"
            );

            var createdTodo = new Todo()
            {
                Id = Guid.NewGuid(),
                Title = "NewToDo",
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
            _todoRepositoryMock.Setup(x => x.Add(createdTodo)).ReturnsAsync(createdTodo);
            _mapperMock.Setup(x => x.Map<TodoResponseDto>(createdTodo)).Returns(responseDto);

            // Act
            var result = await _todoService.AddAsync(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Yap?lacak i? eklendi.", result.Message);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(201, result.StatusCode);
        }
    }
}