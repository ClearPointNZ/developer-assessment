using DeveloperAssessment.Modules.Todos.Core.DAL.EF;
using DeveloperAssessment.Modules.Todos.Core.DAL.Repositories;
using DeveloperAssessment.Modules.Todos.Core.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CA1816

namespace DeveloperAssessment.Modules.Todos.Core.Tests.DAL.Repositories;

public class TodoRepositoryTests : IDisposable
{
    private readonly TodosDbContext _todosDbContext;
    private readonly TodoRepository _todoRepository;

    public TodoRepositoryTests()
    {
        var dbOptions = new DbContextOptionsBuilder<TodosDbContext>()
            .UseInMemoryDatabase(databaseName: "todos")
            .Options;

        _todosDbContext = new TodosDbContext(dbOptions);
        _todoRepository = new TodoRepository(_todosDbContext);
    }

    public async void Dispose()
    {
        // Remove Todos after each test
        _todosDbContext.Todos.RemoveRange(_todosDbContext.Todos);
        await _todosDbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Todo()
    {
        // Arrange
        var existingTodo = new Todo(Guid.NewGuid(), "Test Todo", false);
        _todosDbContext.Todos.Add(existingTodo);
        await _todosDbContext.SaveChangesAsync();

        // Act
        var todo = await _todoRepository.GetByIdAsync(existingTodo.Id);

        // Assert
        todo.Should().BeEquivalentTo(existingTodo);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GetAllAsync_Should_Return_All_Todos(bool showCompleted)
    {
        // Arrange
        var existingTodos = new List<Todo>
        {
            new(Guid.NewGuid(), "Test Todo 1", false),
            new(Guid.NewGuid(), "Test Todo 2", false),
            new(Guid.NewGuid(), "Test Todo 3", true)
        };
        _todosDbContext.Todos.AddRange(existingTodos);
        await _todosDbContext.SaveChangesAsync();

        // Act
        var todos = await _todoRepository.GetAllAsync(showCompleted);

        // Assert
        todos.Should().BeEquivalentTo(showCompleted ? existingTodos : existingTodos.Where(x => !x.IsCompleted));
    }

    [Fact]
    public async Task AddAsync_Should_Add_Todo()
    {
        // Arrange
        var newTodo = new Todo(Guid.NewGuid(), "Test Todo", false);

        // Act
        var createdTodo = await _todoRepository.AddAsync(newTodo);

        // Assert
        createdTodo.Should().BeEquivalentTo(newTodo);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Todo()
    {
        // Arrange
        var existingTodo = new Todo(Guid.NewGuid(), "Test Todo", false);
        _todosDbContext.Todos.Add(existingTodo);
        await _todosDbContext.SaveChangesAsync();

        // Act
        existingTodo.TryUpdate("Updated Todo", true);
        await _todoRepository.UpdateAsync(existingTodo);

        // Assert
        var updatedTodo = await _todosDbContext.Todos.FindAsync(existingTodo.Id);
        updatedTodo.Should().BeEquivalentTo(existingTodo);
    }
}
