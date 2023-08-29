using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace TodoList.Infrastructure.UnitTests;

public class RepositoryTest
{
    protected async void Execute(Func<TodoRepository, Task> action)
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;

        using (var context = new TodoContext(options))
        {
            await context.Database.EnsureCreatedAsync();

            var repository = new TodoRepository(context);

            await action(repository);
        }
    }
}
