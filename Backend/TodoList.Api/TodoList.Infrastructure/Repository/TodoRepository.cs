using Microsoft.EntityFrameworkCore;
using System.Data;
using TodoList.Library;

namespace TodoList.Infrastructure;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context.EnsureNotNull();
    }

    public async Task<IList<TodoItem>> FetchAllAsync()
    {
        return await _context.TodoItems
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<TodoItem>> FetchUncompletedAsync()
    {
        return await _context.TodoItems
                            .Where(x => !x.IsCompleted)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<TodoItem?> FindByIdAsync(Guid id)
    {
        if (Guid.Empty == id)
            return null;

        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem?> FindByDescriptionAndUncompletedAsync(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DuplicateNameException();

        return await _context.TodoItems.FirstOrDefaultAsync(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
    }

    public async Task<Guid> CreateAsync(TodoItem item)
    {
        item.EnsureNotNull();
        item.Id.EnsureNotNullOrDefault();

        var existing = await _context.TodoItems.FindAsync(item.Id);
        if (existing is not null)
            throw new DuplicateNameException();

        var created = await _context.TodoItems.AddAsync(item);

        await _context.SaveChangesAsync();

        return created.Entity.Id;
    }

    public async Task UpdateAsync(TodoItem item)
    {
        item.EnsureNotNullOrDefault();
        item.Id.EnsureNotNullOrDefault();

        var existing = await _context.TodoItems.FindAsync(item.Id);
        if (existing is null)
            throw new DbUpdateConcurrencyException();

        _context.Entry(existing).CurrentValues.SetValues(item);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        id.EnsureNotNullOrDefault();

        var existing = await _context.TodoItems.FindAsync(id);
        if (existing is null)
            throw new DbUpdateConcurrencyException();

        _context.TodoItems.Remove(existing);

        await _context.SaveChangesAsync();
    }
}
