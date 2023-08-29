using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Services;

public static class Registrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}