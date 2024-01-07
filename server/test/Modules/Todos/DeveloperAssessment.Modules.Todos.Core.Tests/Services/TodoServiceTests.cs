using DeveloperAssessment.Modules.Todos.Core.DAL.Repositories;
using DeveloperAssessment.Modules.Todos.Core.DTO;
using DeveloperAssessment.Modules.Todos.Core.Entities;
using DeveloperAssessment.Modules.Todos.Core.Exceptions;
using DeveloperAssessment.Modules.Todos.Core.Mappings;
using DeveloperAssessment.Modules.Todos.Core.Services;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;

namespace DeveloperAssessment.Modules.Todos.Core.Tests.Services;

public class TodoServiceTests
{
    private readonly Mock<ITodoRepository> _mockTodoRepository;
    private readonly Mock<ILogger<TodoService>> _mockLogger;

    private readonly TodoService _todoService;

    public TodoServiceTests()
    {
        _mockTodoRepository = new Mock<ITodoRepository>();
        _mockLogger = new Mock<ILogger<TodoService>>();

        _todoService = new TodoService(_mockTodoRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Todo_When_It_Exists()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new Todo(todoId, "Test Description", false);

        _mockTodoRepository.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync(todo);

        // Act
        var result = await _todoService.GetByIdAsync(todoId);

        // Assert
        todo.Id.Should().Be((Guid)result?.Id!);
        todo.Description.Should().Be(result.Description);
        todo.IsCompleted.Should().Be(result.IsCompleted);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Todo_Does_Not_Exist()
    {
        // Arrange
        var todoId = Guid.NewGuid();

        _mockTodoRepository.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync((Todo?)null);

        // Act
        var result = await _todoService.GetByIdAsync(todoId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Todos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new(Guid.NewGuid(), "Test Description 1", false),
            new(Guid.NewGuid(), "Test Description 2", true),
            new(Guid.NewGuid(), "Test Description 3", false)
        };

        _mockTodoRepository.Setup(x => x.GetAllAsync(false)).ReturnsAsync(todos);

        // Act
        var result = await _todoService.GetAllAsync(false);

        // Assert
        result.Should().HaveCount(3);
        for (var i = 0; i < result.Count; i++)
        {
            result[i].Id.Should().Be(todos[i].Id);
            result[i].Description.Should().Be(todos[i].Description);
            result[i].IsCompleted.Should().Be(todos[i].IsCompleted);
        }
    }

    [Fact]
    public async Task AddAsync_Should_Add_Todo()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new TodoRequestDto
        {
            Id = todoId,
            Description = "Test Description",
            IsCompleted = false
        };

        _mockTodoRepository.Setup(x => x.AddAsync(It.IsAny<Todo>())).ReturnsAsync(todo.ToEntity());

        // Act
        var result = await _todoService.AddAsync(todo);

        // Assert
        result.Id.Should().Be(todoId);
        result.Description.Should().Be(todo.Description);
        result.IsCompleted.Should().Be((bool)todo.IsCompleted);
        VerifyLogger(LogLevel.Information, $"Created a todo with ID: '{result.Id}'");
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Todo_When_Found_And_Has_Changes()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new Todo(todoId, "Test Description", false);
        var todoRequest = new TodoRequestDto
        {
            Id = todoId,
            Description = "Test Description Updated",
            IsCompleted = true
        };

        _mockTodoRepository.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync(todo);
        _mockTodoRepository.Setup(x => x.UpdateAsync(It.IsAny<Todo>()));

        // Act
        await _todoService.UpdateAsync(todoRequest);

        // Assert
        _mockTodoRepository.Verify(x => x.UpdateAsync(It.IsAny<Todo>()), Times.Once);
        VerifyLogger(LogLevel.Information, $"Updated a todo with ID: '{todoId}'");
    }

    [Fact]
    public async Task UpdateAsync_Should_Not_Update_Todo_When_Found_But_No_Changes()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new Todo(todoId, "Test Description", false);
        var todoRequest = new TodoRequestDto
        {
            Id = todoId,
            Description = "Test Description",
            IsCompleted = false
        };

        _mockTodoRepository.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync(todo);
        _mockTodoRepository.Setup(x => x.UpdateAsync(It.IsAny<Todo>()));

        // Act
        await _todoService.UpdateAsync(todoRequest);

        // Assert
        _mockTodoRepository.Verify(x => x.UpdateAsync(It.IsAny<Todo>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_Should_Throw_TodoNotFoundException_When_Todo_Not_Found()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todoRequest = new TodoRequestDto
        {
            Id = todoId,
            Description = "Test Description",
            IsCompleted = false
        };

        _mockTodoRepository.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync((Todo?)null);

        // Act
        var act = async () => await _todoService.UpdateAsync(todoRequest);

        // Assert
        await act.Should().ThrowAsync<TodoNotFoundException>();
    }

    private void VerifyLogger(LogLevel level, string message)
    {
        _mockLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == level),
                0,
                It.Is<It.IsAnyType>((@o, @t) => @o.ToString() == message && @t.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }
}
