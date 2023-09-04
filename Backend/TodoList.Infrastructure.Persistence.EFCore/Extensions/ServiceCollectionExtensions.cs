using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Domain.Repositories;
using TodoList.Infrastructure.Persistence.EFCore.Repositories;

namespace TodoList.Infrastructure.Persistence.EFCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryPersistence(this IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>(opt => opt.UseInMemoryDatabase("TodoItemsDB"));
            services.AddTransient<ITodoListRepository, TodoListRepository>();
        }
    }
}
