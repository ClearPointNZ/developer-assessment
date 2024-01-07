using System.Runtime.CompilerServices;
using DeveloperAssessment.Modules.Todos.Core.DAL.EF;
using DeveloperAssessment.Modules.Todos.Core.Entities;
using Microsoft.EntityFrameworkCore;

[assembly:InternalsVisibleTo("DeveloperAssessment.Modules.Todos.Core.Tests")]
namespace DeveloperAssessment.Modules.Todos.Core.DAL.Repositories;

internal sealed class TodoRepository(TodosDbContext context) : ITodoRepository
{
    private readonly DbSet<Todo> _todos = context.Todos;

    public async Task<Todo?> GetByIdAsync(Guid id) => await _todos.FindAsync(id);

    public async Task<IReadOnlyList<Todo>> GetAllAsync(bool showCompleted) =>
        showCompleted ?
            await _todos.ToListAsync() :
            await _todos.Where(x => !x.IsCompleted).ToListAsync();

    public async Task<Todo> AddAsync(Todo todo)
    {
        await _todos.AddAsync(todo);
        await context.SaveChangesAsync();

        return todo;
    }

    public Task UpdateAsync(Todo todo)
    {
        _todos.Update(todo);
        return context.SaveChangesAsync();
    }
}
