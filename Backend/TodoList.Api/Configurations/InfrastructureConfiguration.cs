using Microsoft.Extensions.DependencyInjection;
using TodoList.Infrastructure.Application.Services.Extensions;
using TodoList.Infrastructure.Persistence.EFCore.Extensions;

namespace TodoList.Api.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAppServices();
        }
        public static void AddEfCoreAsPersistence(this IServiceCollection services)
        {
            services.AddInMemoryPersistence();
        }
    }
}
