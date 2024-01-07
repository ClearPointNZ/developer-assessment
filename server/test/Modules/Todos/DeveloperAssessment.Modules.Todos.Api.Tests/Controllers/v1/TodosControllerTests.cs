using DeveloperAssessment.Modules.Todos.Api.Controllers.v1;
using DeveloperAssessment.Modules.Todos.Core.DTO;
using DeveloperAssessment.Modules.Todos.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeveloperAssessment.Modules.Todos.Api.Tests.Controllers.v1;

public class TodosControllerTests
{
    private readonly Mock<ITodoService> _mockTodoService;

    private readonly TodosController _todosController;

    public TodosControllerTests()
    {
        _mockTodoService = new Mock<ITodoService>();

        _todosController = new TodosController(_mockTodoService.Object);
    }

    [Fact]
    public async Task GetById_Should_Return_Todo_If_Exists()
    {
        // Arrange
        var existingTodo = new TodoResponseDto
        {
            Id = Guid.NewGuid(),
            Description = "Test Description",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null
        };

        _mockTodoService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingTodo);

        // Act
        var result = await _todosController.GetById(existingTodo.Id);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var response = (result.Result as OkObjectResult)?.Value;
        response.Should().BeEquivalentTo(existingTodo);
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_If_Todo_Does_Not_Exist()
    {
        // Arrange
        _mockTodoService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((TodoResponseDto)null!);

        // Act
        var result = await _todosController.GetById(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAll_Should_Return_Todos()
    {
        // Arrange
        var todos = new List<TodoResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Test Description",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                CompletedAt = null
            },
            new()
            {
                Id = Guid.NewGuid(),
                Description = "Test Description",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                CompletedAt = null
            }
        };

        _mockTodoService.Setup(x => x.GetAllAsync(It.IsAny<bool>())).ReturnsAsync(todos);

        // Act
        var result = await _todosController.GetAll();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var response = (result.Result as OkObjectResult)?.Value;
        response.Should().BeEquivalentTo(todos);
    }

    [Fact]
    public async Task Post_Should_Return_New_Todo()
    {
        // Arrange
        var newTodo = new TodoResponseDto
        {
            Id = Guid.NewGuid(),
            Description = "Test Description",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null
        };

        _mockTodoService.Setup(x => x.AddAsync(It.IsAny<TodoRequestDto>())).ReturnsAsync(newTodo);

        // Act
        var result = await _todosController.Post(new TodoRequestDto());

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var response = (result as CreatedAtActionResult)?.Value;
        response.Should().BeEquivalentTo(newTodo);
    }

    [Fact]
    public async Task Put_Should_Return_NoContent()
    {
        // Arrange
        _mockTodoService.Setup(x => x.UpdateAsync(It.IsAny<TodoRequestDto>()));

        // Act
        var result = await _todosController.Put(Guid.NewGuid(), new TodoRequestDto());

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
