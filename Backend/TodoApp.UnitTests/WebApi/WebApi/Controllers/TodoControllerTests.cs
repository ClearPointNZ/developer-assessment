using Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Services;
using System.Net;
using TodoApp.Exceptions;
using TodoApp.Models;
using TodoList.Api.Controllers;

namespace WebApi.Controllers
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoService> _mockService;

        public TodoControllerTests()
        {
            _mockService = new Mock<ITodoService>();
        }

        [Fact]
        public async Task GetTodo_ValidRequest_ShouldReturnOk()
        {
            // ARRANGE
            var todo = new TodoItem
            {
                Description = "something"
            };

            _mockService.Setup(s => s.Get(It.IsAny<Guid>()))
                .ReturnsAsync(todo);


            var sut = new TodoController(_mockService.Object);

            // ACT
            var result = (OkObjectResult) await sut.GetTodoItem(Guid.NewGuid());

            // ASSERT
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeEquivalentTo(todo);
        }

        [Fact]
        public async Task GetTodo_NoTodoFound_ShouldReturnNotFound()
        {
            // ARRANGE
            _mockService.Setup(s => s.Get(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);


            var sut = new TodoController(_mockService.Object);

            // ACT
            var result = (NotFoundResult)await sut.GetTodoItem(Guid.NewGuid());

            // ASSERT
            result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        // POST
        [Fact]
        public async Task PostTodo_ExistingDescription_ShouldReturnBadRequest()
        {
            // ARRANGE
            var exception = new ExistingDescriptionException();

            _mockService.Setup(s => s.Create(It.IsAny<TodoItem>()))
                .ThrowsAsync(exception);


            var sut = new TodoController(_mockService.Object);

            // ACT
            var result = (BadRequestObjectResult) await sut.PostTodoItem(new TodoRequest { Description = "test", IsCompleted = false});

            // ASSERT
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            result.Value.Should().Be(exception.Message);
        }

        [Fact]
        public async Task PostTodo_NewItem_ShouldReturnOk_WithNewlyCreatedItem()
        {
            // ARRANGE
            var description = "new description";
            var isCompleted = false;
            var newId = Guid.NewGuid();
            
            var request = new TodoRequest
            {
                Description = description,
            };

            var mockResponse = new TodoItem
            {
                Description = description,
                IsCompleted = isCompleted,
                Id = newId
            };

            _mockService.Setup(s => s.Create(It.IsAny<TodoItem>()))
                .ReturnsAsync(mockResponse);


            var sut = new TodoController(_mockService.Object);

            // ACT
            var result = (OkObjectResult)await sut.PostTodoItem(new TodoRequest { Description = description, IsCompleted = isCompleted });

            // ASSERT
            result.StatusCode.Should().Be((int)HttpStatusCode.Created);
            result.Value.As<TodoItem>().Description.Should().Be(mockResponse.Description);
            result.Value.As<TodoItem>().IsCompleted.Should().Be(mockResponse.IsCompleted);
            result.Value.As<TodoItem>().Id.Should().Be(mockResponse.Id);

            _mockService.Verify(s => s.Create(It.Is<TodoItem>(item => item.Description == request.Description && item.IsCompleted == request.IsCompleted)), Times.Once);
        }

    }
}
