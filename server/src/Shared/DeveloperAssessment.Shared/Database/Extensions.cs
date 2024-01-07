using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperAssessment.Shared.Database;

/// <summary>
/// This class contains extension methods for adding Postgres support to the IServiceCollection.
/// </summary>
public static class Extensions
{
    private const string SectionName = "postgres";

    /// <summary>
    /// Adds Postgres Options to the IServiceCollection and runs DB migrations in the background.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the Postgres support to.</param>
    /// <param name="configuration">The IConfiguration containing the Postgres options.</param>
    /// <returns>The updated IServiceCollection.</returns>
    internal static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetSection(SectionName));
        services.AddHostedService<DbContextAppInitialiser>();

        // Temporary fix for EF Core issue related to https://github.com/npgsql/efcore.pg/issues/2000
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    /// <summary>
    /// Adds Postgres support to the IServiceCollection for a specific DbContext type.
    /// </summary>
    /// <typeparam name="T">The type of the DbContext.</typeparam>
    /// <param name="services">The IServiceCollection to add the Postgres support to.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        var connectionString = configuration[$"{SectionName}:{nameof(PostgresOptions.ConnectionString)}"];

        services.AddDbContext<T>(x => x.UseNpgsql(connectionString));

        return services;
    }
}
