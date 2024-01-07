using DeveloperAssessment.Shared.Api;
using DeveloperAssessment.Shared.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using DeveloperAssessment.Shared.Database;
using DeveloperAssessment.Shared.Exceptions;

namespace DeveloperAssessment.Shared;

// Shared container/middleware configuration
public static class Extensions
{
    private const string CorsPolicyName = "AllowAllHeaders";

    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddErrorHandling();
        services.AddPostgres(configuration);

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName,
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddApiVersioningAndExplorer();
        services.AddSwagger();

        return services;
    }

    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        app.UseSwaggerMiddleware();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(CorsPolicyName);
        app.UseAuthorization();

        return app;
    }
}
