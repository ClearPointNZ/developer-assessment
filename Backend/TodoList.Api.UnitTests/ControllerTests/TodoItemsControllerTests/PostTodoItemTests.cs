using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Controllers;
using TodoList.Services.Modules.TodoListModule;
using TodoList.Services.Modules.TodoListModule.Models;
using TodoList.Services.Modules.TodoListModule.ViewModels;
using TodoList.Services.Shared;
using Xunit;

namespace TodoList.Api.UnitTests.ControllerTests.TodoItemsControllerTests
{
    public class PostTodoItemTests
    {
        [Fact]
        public async Task ShouldReturnCreatedItemWhenValidInputPassed()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var description = "Test";
            var todoItemModel = new CreateTodoItemModel { Description = description };
            var itemCreated = new GetTodoItemViewModel { Description = description };
            var serviceResult = new ServiceResult<GetTodoItemViewModel>();
            serviceResult.ToResourceCreatedResult(itemCreated);

            var fakeService = A.Fake<ITodoListService>();
            A.CallTo(() => fakeService.CreateTodoItem(todoItemModel, cancellationToken))
                .Returns(Task.FromResult(serviceResult));

            var controller = new TodoItemsController(fakeService);

            // Act
            var actionResult = await controller.PostTodoItem(todoItemModel, cancellationToken);

            // Assert
            A.CallTo(() => fakeService.CreateTodoItem(todoItemModel, cancellationToken)).MustHaveHappenedOnceExactly();
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            Assert.Equal(nameof(controller.GetTodoItem), createdAtActionResult.ActionName);
            Assert.Equal(itemCreated, createdAtActionResult.Value);
        }

        [Fact]
        public async Task ShoudlReturnBadRequestWhenInvalidInputPassed()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            CreateTodoItemModel todoItemModel = null;

            var serviceResult = new ServiceResult<GetTodoItemViewModel>();
            serviceResult.BadRequestWithMessage("Description is required");

            var fakeService = A.Fake<ITodoListService>();
            A.CallTo(() => fakeService.CreateTodoItem(todoItemModel, cancellationToken))
                .Returns(Task.FromResult(serviceResult));

            var controller = new TodoItemsController(fakeService);

            // Act
            var actionResult = await controller.PostTodoItem(todoItemModel, cancellationToken);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal("Description is required", badRequestResult.Value);
        }
    }
}
