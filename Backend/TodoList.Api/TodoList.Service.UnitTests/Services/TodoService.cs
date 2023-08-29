using AutoFixture.Xunit2;
using Moq;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Testing;
using Xunit;

namespace TodoList.Services.UnitTests;

public class TodoServiceTests
{
    #region FetchUncompleted

    [Theory, ServiceInjection]
    public async void FetchUncompleted_ShouldReturnValue(
        [Frozen] Mock<ITodoRepository> repository
        , [Frozen] TodoItem value
        , TodoService svc)
    {
        // ARRANGE
        repository.Setup(r => r.FetchUncompletedAsync())
            .ReturnsAsync(new[] { value });

        // ACT
        var returnValue = await svc.FetchUncompletedAsync();

        // ASSERT
        Assert.Single(returnValue);
        Assert.Equal(value, returnValue.First());
        repository.Verify(m => m.FetchUncompletedAsync(), Times.Once);
    }

    #endregion

    #region FetchById

    [Theory, ServiceInjection]
    public async void FindById_ShouldReturnValue(
        [Frozen] Mock<ITodoRepository> repository
        , [Frozen] TodoItem value
        , TodoService svc)
    {
        // ARRANGE
        repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>()))
                  .ReturnsAsync(value);

        // ACT
        var returnValue = await svc.FindByIdAsync(Guid.NewGuid());

        // ASSERT
        Assert.Equal(returnValue, value);
        repository.Verify(m => m.FindByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Theory, ServiceInjection]
    public async void FindById_ShouldReturnNull_IfNotExist(
        [Frozen] Mock<ITodoRepository> repository
        , TodoService svc)
    {
        // ARRANGE
        repository.Setup(r => r.FindByIdAsync(It.IsAny<Guid>()))
                  .ReturnsAsync((TodoItem)null);

        // ACT
        var returnValue = await svc.FindByIdAsync(Guid.NewGuid());

        // ASSERT
        Assert.Null(returnValue);
        repository.Verify(m => m.FindByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Theory, ServiceInjection]
    public async void FindById_ShouldThrowException_IfParameterIsEmpty(
        [Frozen] Mock<ITodoRepository> repository
        , TodoService svc)
    {
        // ACT
        var returnValue = await svc.FindByIdAsync(Guid.NewGuid());

        // ASSERT
        Assert.Null(returnValue);
        repository.Verify(m => m.FindByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    #endregion

    #region Create

    [Theory, ServiceInjection]
    public async void Create_ShouldSaveValue(
        [Frozen] Mock<ITodoRepository> repository
        , [Frozen] TodoItem value
        , TodoService svc)
    {
        // ARRANGE
        value.Id = Guid.Empty;
        var valueId = Guid.NewGuid();
        repository.Setup(r => r.CreateAsync(It.IsAny<TodoItem>()))
                    .ReturnsAsync(valueId);

        // ACT
        var returnValue = await svc.CreateAsync(value);

        // ASSERT
        Assert.NotEqual(valueId, value.Id);
        repository.Verify(m => m.CreateAsync(value), Times.Once);
        repository.Verify(m => m.FindByDescriptionAndUncompletedAsync(It.IsAny<string>()), Times.Once);
    }

    [Theory, ServiceInjection]
    public async void Create_ShouldThrowException_IfExists(
        [Frozen] Mock<ITodoRepository> repository
        , TodoService svc
        , TodoItem value)
    {
        // ARRANGE
        value.IsCompleted = false;
        repository.Setup(r => r.CreateAsync(It.IsAny<TodoItem>()));
        repository.Setup(r => r.FindByDescriptionAndUncompletedAsync(It.IsAny<string>()))
            .ReturnsAsync(value);

        // ACT
        await Assert.ThrowsAsync<DuplicateNameException>(async () =>
            await svc.CreateAsync(value));

        // ASSERT
        repository.Verify(m => m.CreateAsync(It.IsAny<TodoItem>()), Times.Never);
        repository.Verify(m => m.FindByDescriptionAndUncompletedAsync(It.IsAny<string>()), Times.Once);
    }

    [Theory, ServiceInjection]
    public async void Create_ShouldThrowException_IfParameterIsEmpty(
        [Frozen] Mock<ITodoRepository> repository
        , TodoService svc)
    {
        // ARRANGE
        repository.Setup(r => r.CreateAsync(It.IsAny<TodoItem>()));

        // ACT
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await svc.CreateAsync(null));

        // ASSERT
        repository.Verify(m => m.CreateAsync(It.IsAny<TodoItem>()), Times.Never);
        repository.Verify(m => m.FindByDescriptionAndUncompletedAsync(It.IsAny<string>()), Times.Never);
    }

    #endregion

    #region Update

    [Theory, ServiceInjection]
    public async void Update_ShouldSaveValue(
        [Frozen] Mock<ITodoRepository> repository
        , [Frozen] TodoItem value
        , TodoService svc)
    {
        // ARRANGE
        repository.Setup(r => r.UpdateAsync(It.IsAny<TodoItem>()))
                    .Returns(Task.CompletedTask);

        // ACT
        await svc.UpdateAsync(value);

        // ASSERT
        repository.Verify(m => m.UpdateAsync(value), Times.Once);
    }

    [Theory, ServiceInjection]
    public async void Update_ShouldThrowException_IfParameterIsEmpty(
        [Frozen] Mock<ITodoRepository> repository
        , TodoService svc)
    {
        // ARRANGE
        repository.Setup(r => r.UpdateAsync(It.IsAny<TodoItem>()));

        // ACT
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await svc.UpdateAsync(null));

        // ASSERT
        repository.Verify(m => m.UpdateAsync(It.IsAny<TodoItem>()), Times.Never);
    }

    #endregion
}
