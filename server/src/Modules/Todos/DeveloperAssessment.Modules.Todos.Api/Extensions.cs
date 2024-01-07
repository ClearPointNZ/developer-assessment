using DeveloperAssessment.Modules.Todos.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperAssessment.Modules.Todos.Api;

public static class Extensions
{
    public static IServiceCollection AddTodosModule(this IServiceCollection services)
    {
        services.AddCoreLayer();

        return services;
    }

    public static IApplicationBuilder UseTodosModule(this IApplicationBuilder app)
    {
        return app;
    }
}
