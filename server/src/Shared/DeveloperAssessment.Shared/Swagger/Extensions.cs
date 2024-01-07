using Asp.Versioning.ApiExplorer;
using DeveloperAssessment.Shared.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperAssessment.Shared.Swagger;

internal static class Extensions
{
    private const string ApiName = "DeveloperAssessment";

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            // Only match on the 'Api' assembly for each module
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.GetName().Name?.Matches($"{ApiName}.Modules.*.Api") ?? false);

            // Add XML comments to Swagger docs for each module
            foreach (var assembly in assemblies)
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath);
            }

            options.OperationFilter<SwaggerDefaultValues>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // Add Swagger docs for all API versions
            var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = $"{ApiName} {description.GroupName}";
                options.SwaggerEndpoint(url, name);
            }
        });

        return app;
    }
}
