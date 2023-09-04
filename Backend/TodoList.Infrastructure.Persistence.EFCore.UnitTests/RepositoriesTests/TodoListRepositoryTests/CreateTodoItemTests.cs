using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Persistence.EFCore.Repositories;

namespace TodoList.Infrastructure.Persistence.EFCore.UnitTests.RepositoriesTests.TodoListRepositoryTests
{
    public class CreateTodoItemTests
    {
        [Fact]
        public async Task ShoudlCreateAnItemWhenCalled()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase(databaseName: "CreateTodoItemDatabase")
                .Options;
            var id = Guid.NewGuid();
            var description = "Test";

            using (var context = new TodoListContext(options))
            {
                var repository = new TodoListRepository(context);
                var item = new TodoItem { Id = id, Description = description };

                // Act
                await repository.CreateTodoItem(item, CancellationToken.None);

                // Assert
                var items = context.TodoItems;
                Assert.Single(items);
                Assert.Equal(id, items.First().Id);
                Assert.Equal(description, items.First().Description);
            }
        }
    }
}
