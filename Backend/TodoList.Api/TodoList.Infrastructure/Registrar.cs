using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infrastructure;

namespace TodoList;

public static class Registrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TodoContext>(options =>
        {
            options.UseInMemoryDatabase("Ping_ClearPoint_Assessment");

        });

        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}