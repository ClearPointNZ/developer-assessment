using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Contexts;

namespace TodoApp
{
    public static class InfrastructureRegistrar
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("Todo"))
                .AddScoped<ITodoContext, TodoContext>();

            return services;
        }
    }
}
