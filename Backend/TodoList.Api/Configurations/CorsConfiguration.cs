using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Api.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder applicationBuilder, string poliocyName)
        {
            applicationBuilder.UseCors(poliocyName);
            return applicationBuilder;
        }
    }
}
