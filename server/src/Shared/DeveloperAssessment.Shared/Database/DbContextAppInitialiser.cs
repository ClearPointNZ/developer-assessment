using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeveloperAssessment.Shared.Database;

/// <summary>
/// Runs database migrations for each DbContext in the application.
/// </summary>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="logger">The logger.</param>
internal sealed class DbContextAppInitialiser(IServiceProvider serviceProvider, ILogger<DbContextAppInitialiser> logger) : IHostedService
{
    /// <summary>
    /// Starts the initialization process.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        using var scope = serviceProvider.CreateScope();

        foreach (var dbContextType in dbContextTypes)
        {
            if (scope.ServiceProvider.GetService(dbContextType) is not DbContext dbContext)
            {
                continue;
            }

            logger.LogInformation("Running migration for DB context: {Name}", dbContext.GetType().Name);

            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Stops the initialization process.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
