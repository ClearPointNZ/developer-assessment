using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Infrastructure.Persistence.EFCore.UnitTests.RepositoriesTests.TodoListRepositoryTests
{
    public class CreateTodoItemTests
    {
        [Fact]
        public async Task CreateTodoItem_Should_Add_Item_To_Context()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoListContext>()
                .UseInMemoryDatabase(databaseName: "CreateTodoItem_Database")
                .Options;

            using (var context = new TodoListContext(options))
            {
                var repository = new TodoListRepository(context);
                var item = new TodoItem { /* Initialize your item properties */ };

                // Act
                await repository.CreateTodoItem(item, CancellationToken.None);

                // Assert
                Assert.Single(context.TodoItems);
            }
        }
    }
}
