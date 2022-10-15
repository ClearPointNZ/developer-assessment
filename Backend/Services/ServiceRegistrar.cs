using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace TodoApp
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }
    }
}
