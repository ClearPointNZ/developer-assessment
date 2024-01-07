using DeveloperAssessment.Modules.Todos.Core.DAL.Repositories;
using DeveloperAssessment.Modules.Todos.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using DeveloperAssessment.Modules.Todos.Core.DAL.EF;
using DeveloperAssessment.Shared.Database;

namespace DeveloperAssessment.Modules.Todos.Core;

public static class Extensions
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddPostgres<TodosDbContext>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}
