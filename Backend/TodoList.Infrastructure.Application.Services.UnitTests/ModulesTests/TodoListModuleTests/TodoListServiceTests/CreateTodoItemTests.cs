using FakeItEasy;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;
using TodoList.Infrastructure.Application.Services.Modules.TodoListModule;
using TodoList.Services.Modules.TodoListModule.Models;
using TodoList.Services.Shared;

namespace TodoList.Infrastructure.Application.Services.UnitTests.ModulesTests.TodoListModuleTests.TodoListServiceTests
{
    public class CreateTodoItemTests
    {

        [Fact]
        public async Task ShouldReturnBadRequestWhenEmptyInputPassed()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            CreateTodoItemModel itemModel = null;

            var fakeRepository = A.Fake<ITodoListRepository>();
            var sut = new TodoListService(fakeRepository);

            // Act
            var result = await sut.CreateTodoItem(itemModel, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResultCode.BadRequest, result.ResultCode);
        }

        [Fact]
        public async Task ShouldReturnInternalServerErrorWhenNothingReturnedFromRepository()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var itemModel = new CreateTodoItemModel();
            TodoItem item = null;

            var fakeRepository = A.Fake<ITodoListRepository>();
            A.CallTo(() => fakeRepository.CreateTodoItem(A<TodoItem>._, cancellationToken))
                .Returns(item);

            var sut = new TodoListService(fakeRepository);

            // Act
            var result = await sut.CreateTodoItem(itemModel, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResultCode.InternalServerError, result.ResultCode);
        }

        [Fact]
        public async Task ShouldReturnResourceCreatedResultWhenValidInputPassed()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var description = "Test";
            var itemModel = new CreateTodoItemModel { Description = description };

            var fakeRepository = A.Fake<ITodoListRepository>();
            A.CallTo(() => fakeRepository.CreateTodoItem(A<TodoItem>._, cancellationToken))
                .Returns(Task.FromResult(new TodoItem { Description = description }));

            var service = new TodoListService(fakeRepository);

            // Act
            var result = await service.CreateTodoItem(itemModel, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Equal(description, result.Result.Description);
        }
    }
}
