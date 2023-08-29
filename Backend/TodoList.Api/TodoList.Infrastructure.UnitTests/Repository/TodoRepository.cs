using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using TodoList.Testing;
using Xunit;

namespace TodoList.Infrastructure.UnitTests.Repositories;

public class TodoItemRepositoryTest : RepositoryTest
{
    #region FetchAllAsync

    [Theory, ServiceInjection]
    public void FetchAll_Should_ReturnAll(
        TodoItem value1
        , TodoItem value2)
    {
        Execute(async repository =>
        {
            // ARRANGE
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FetchAllAsync();

            // ASSERT
            Assert.Equal(2, result.Count);
            Assert.Equivalent(result.First(t => t.Id == value1.Id), value1);
            Assert.Equivalent(result.First(t => t.Id == value2.Id), value2);
        });
    }

    [Theory, ServiceInjection]
    public void FetchAll_Should_ReturnEmpty(
        TodoItem value1
        , TodoItem value2)
    {
        Execute(async repository =>
        {
            // ACT
            var result = await repository.FetchAllAsync();

            // ASSERT
            Assert.Empty(result);
        });
    }

    #endregion

    #region FetchUncompletedAsync

    [Theory, ServiceInjection]
    public void FetchUncompleted_Should_ReturnAll(
        TodoItem value1
        , TodoItem value2)
    {
        Execute(async repository =>
        {
            // ARRANGE
            value1.IsCompleted = true;
            value2.IsCompleted = false;
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FetchUncompletedAsync();

            // ASSERT
            Assert.Single(result);
            Assert.Equivalent(result[0], value2);
        });
    }

    #endregion

    #region FindByDescriptionAndUncompletedAsync

    [Theory, ServiceInjection]
    public void FindByDescriptionAndUncompletedAsync_Should_MeetCriteria(
        TodoItem value1
        , TodoItem value2
        , string description)
    {
        Execute(async repository =>
        {
            // ARRANGE
            value1.Description = description;
            value1.IsCompleted = true;
            value2.Description = description;
            value2.IsCompleted = false;
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FindByDescriptionAndUncompletedAsync(description);

            // ASSERT
            Assert.Equivalent(result, value2);
        });
    }

    [Theory, ServiceInjection]
    public void FindByDescriptionAndUncompletedAsync_ShouldFind_EvenUpperCase(
        TodoItem value1
        , TodoItem value2
        , string description)
    {
        Execute(async repository =>
        {
            // ARRANGE
            value1.Description = description;
            value1.IsCompleted = true;
            value2.Description = description;
            value2.IsCompleted = false;
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FindByDescriptionAndUncompletedAsync(description.ToUpperInvariant());

            // ASSERT
            Assert.Equivalent(result, value2);
        });
    }

    [Theory, ServiceInjection]
    public void FindByDescriptionAndUncompletedAsync_ShouldReturnEmpty_IfNotMeetCriteria(
        TodoItem value1
        , TodoItem value2)
    {
        Execute(async repository =>
        {
            // ARRANGE
            value1.IsCompleted = false;
            value2.IsCompleted = false;
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FindByDescriptionAndUncompletedAsync("ABC");

            // ASSERT
            Assert.Null(result);
        });
    }

    #endregion

    #region FindByIdAsync

    [Theory, ServiceInjection]
    public void FetchById_Should_ReturnAll(
        TodoItem value1
        , TodoItem value2)
    {
        Execute(async repository =>
        {
            // ARRANGE
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FindByIdAsync(value2.Id);

            // ASSERT
            Assert.Equal(result, value2);
        });
    }

    [Theory, ServiceInjection]
    public void FetchById_Should_ReturnNull(
        TodoItem value1
        , TodoItem value2)
    {
        Execute(async repository =>
        {
            // ARRANGE
            await repository.CreateAsync(value1);
            await repository.CreateAsync(value2);

            // ACT
            var result = await repository.FindByIdAsync(Guid.Empty);

            // ASSERT
            Assert.Null(result);
        });
    }

    #endregion

    #region CreateAsync

    [Theory, ServiceInjection]
    public void Create_Should_ReturnAll(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ACT
            var result = await repository.CreateAsync(value1);

            // ASSERT
            var db = await repository.FetchAllAsync();
            Assert.Single(db);
            Assert.Equivalent(db[0].Id, result);
        });
    }

    [Theory, ServiceInjection]
    public void Create_ShouldThrowException_IfExists(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ARRANGE
            var result = await repository.CreateAsync(value1);

            // ACT
            await Assert.ThrowsAsync<DuplicateNameException>(async () =>
                await repository.CreateAsync(value1));
        });
    }

    [Theory, ServiceInjection]
    public void Create_ShouldThrowException_IfEmpty()
    {
        Execute(async repository =>
        {
            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await repository.CreateAsync(null));
        });
    }

    [Theory, ServiceInjection]
    public void Create_ShouldThrowException_IfIdEmpty(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ARRANGE
            value1.Id = Guid.Empty;

            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await repository.CreateAsync(value1));
        });
    }

    #endregion

    #region UpdateAsync

    [Theory, ServiceInjection]
    public void Update_Should_Success(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ARRANGE
            await repository.CreateAsync(value1);
            value1.Description = "NEW";
            value1.IsCompleted = !value1.IsCompleted;

            // ACT
            await repository.UpdateAsync(value1);

            // ASSERT
            var db = await repository.FetchAllAsync();
            Assert.Single(db);
            Assert.Equivalent(db[0], value1);
        });
    }

    [Theory, ServiceInjection]
    public void Update_ShouldThrowException_IfNotExists(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ACT
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await repository.UpdateAsync(value1));
        });
    }

    [Theory, ServiceInjection]
    public void Update_ShouldThrowException_IfEmpty()
    {
        Execute(async repository =>
        {
            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await repository.UpdateAsync(null));
        });
    }

    [Theory, ServiceInjection]
    public void Update_ShouldThrowException_IfIdEmpty(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ARRANGE
            value1.Id = Guid.Empty;

            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await repository.UpdateAsync(value1));
        });
    }

    #endregion

    #region DeleteAsync

    [Theory, ServiceInjection]
    public void Delete_Should_Success(
        TodoItem value1)
    {
        Execute(async repository =>
        {
            // ARRANGE
            await repository.CreateAsync(value1);

            // ACT
            await repository.DeleteAsync(value1.Id);

            // ASSERT
            var db = await repository.FetchAllAsync();
            Assert.Empty(db);
        });
    }

    [Theory, ServiceInjection]
    public void Delete_ShouldThrowException_IfNotExists()
    {
        Execute(async repository =>
        {
            // ACT
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await repository.DeleteAsync(Guid.NewGuid()));
        });
    }

    [Theory, ServiceInjection]
    public void Delete_ShouldThrowException_IfIdEmpty()
    {
        Execute(async repository =>
        {
            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await repository.DeleteAsync(Guid.Empty));
        });
    }

    #endregion
}
