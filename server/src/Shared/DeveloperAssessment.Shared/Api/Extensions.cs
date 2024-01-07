using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperAssessment.Shared.Api;

internal static class Extensions
{
    // https://mohsen.es/api-versioning-and-swagger-in-asp-net-core-7-0-fe45f67d8419
    public static IServiceCollection AddApiVersioningAndExplorer(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ReportApiVersions = true;
            o.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
