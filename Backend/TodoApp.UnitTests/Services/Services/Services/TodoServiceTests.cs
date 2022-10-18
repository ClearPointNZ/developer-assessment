using Core.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services.Services;
using System;
using System.Linq.Expressions;
using TodoApp.Contexts;
using TodoApp.Exceptions;

namespace Testings.Services
{
    public class TodoServiceTests
    {
        private Mock<ITodoContext> _mockContext;

        public TodoServiceTests()
        {
            _mockContext = new Mock<ITodoContext>();
        }

        [Fact]
        public async Task Create_NewTodoItem_ShouldCreate()
        {
            // ARRANGE
            _mockContext
                .Setup(c => c.DescriptionExists(It.IsAny<string>()))
                .ReturnsAsync(false);

            var todo = new TodoItem { Description = "test", IsCompleted = true };

            var sut = new TodoService(_mockContext.Object);

            // ACT
            await sut.Create(todo);

            // ASSERT
            _mockContext.Verify(c => c.Create(It.Is<TodoItem>(item => item.Description == todo.Description && item.IsCompleted == todo.IsCompleted)), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_ItemExists_ShouldThrowExistingDescriptionException()
        {
            // ARRANGE
            _mockContext
                .Setup(c => c.DescriptionExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            var todo = new TodoItem { Description = "test", IsCompleted = true };

            var sut = new TodoService(_mockContext.Object);

            // ACT
            await Assert.ThrowsAsync<ExistingDescriptionException>(() => sut.Create(todo));

            // ASSERT
            _mockContext.Verify(c => c.Create(It.Is<TodoItem>(item => item.Description == todo.Description && item.IsCompleted == todo.IsCompleted)), Times.Never);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Create_NullTodoItem_ShouldNotCreate()
        {
            // ARRANGE
            var sut = new TodoService(_mockContext.Object);

            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Create(null));

            // ASSERT
            _mockContext.Verify(c => c.Create(It.IsAny<TodoItem>()), Times.Never);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task Create_EmptyOrWhiteStringTodoItemDescription_ShouldNotCreate(string description)
        {
            // ARRANGE
            var sut = new TodoService(_mockContext.Object);

            // ACT
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Create(new TodoItem { Description = description}));

            // ASSERT
            _mockContext.Verify(c => c.Create(It.IsAny<TodoItem>()), Times.Never);
            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Delete_TodoItemExists_ShouldDelete()
        {
            // ARRANGE
            var existingTodo = new TodoItem {
                Description = "test",
                IsCompleted = true,
                Id = Guid.NewGuid()
            };

            _mockContext
                .Setup(c => c.Get(It.IsAny<Guid>()))
                .ReturnsAsync(existingTodo);

            var sut = new TodoService(_mockContext.Object);

            // ACT
            await sut.Delete(existingTodo.Id);

            // ASSERT
            _mockContext.Verify(c => c.Get(It.Is<Guid>(id => id == existingTodo.Id)), Times.Once);

            _mockContext.Verify(c => c.Delete(It.Is<TodoItem>(
                item =>
                    item.Id == existingTodo.Id &&
                    item.Description == existingTodo.Description)), Times.Once);

            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Delete_TodoItemNotExist_ShouldNotDelete()
        {
            // ARRANGE
            var sut = new TodoService(_mockContext.Object);

            _mockContext
                .Setup(c => c.Get(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            // ACT
            await sut.Delete(Guid.NewGuid());

            // ASSERT
            _mockContext.Verify(c => c.Get(It.IsAny<Guid>()), Times.Once);

            _mockContext.Verify(c => c.Create(It.IsAny<TodoItem>()), Times.Never);

            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Update_TodoExists_ShouldUpdate()
        {
            // ARRANGE
            var existingId = Guid.NewGuid();

            var existingTodo = new TodoItem
            {
                Description = "test",
                IsCompleted = true,
                Id = existingId
            };

            _mockContext
                .Setup(c => c.Get(It.IsAny<Guid>()))
                .ReturnsAsync(existingTodo);

            var sut = new TodoService(_mockContext.Object);

            var newTodo = new TodoItem { Description = "new description", IsCompleted = false, Id = existingId };

            // ACT
            await sut.Update(newTodo);

            // ASSERT
            _mockContext.Verify(c => c.Get(It.Is<Guid>(id => id == existingTodo.Id)), Times.Once);

            _mockContext.Verify(c => c.Update(It.Is<TodoItem>(
                item =>
                    item.Id == newTodo.Id &&
                    item.Description == newTodo.Description &&
                    item.IsCompleted == newTodo.IsCompleted)), Times.Once);

            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_TodoNotExist_ShouldNotUpdate()
        {
            // ARRANGE

            _mockContext
                .Setup(c => c.Get(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            var sut = new TodoService(_mockContext.Object);

            var newTodo = new TodoItem { Description = "new description", IsCompleted = false, Id = Guid.NewGuid() };

            // ACT
            await sut.Update(newTodo);

            // ASSERT
            _mockContext.Verify(c => c.Get(It.Is<Guid>(id => id == newTodo.Id)), Times.Once);

            _mockContext.Verify(c => c.Update(It.IsAny<TodoItem>()), Times.Never);

            _mockContext.Verify(c => c.SaveChangesAsync(), Times.Never);
        }
    }

}
