using DeveloperAssessment.Modules.Todos.Core.Entities;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace DeveloperAssessment.Modules.Todos.Core.DAL.EF;

public class TodosDbContext(DbContextOptions<TodosDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("todos")
            .ApplyConfigurationsFromAssembly(typeof(TodosDbContext).Assembly);
    }
}
