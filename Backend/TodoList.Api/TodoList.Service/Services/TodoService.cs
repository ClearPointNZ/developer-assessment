using System.Data;
using TodoList.Library;

namespace TodoList.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository.EnsureNotNull();
    }

    public async Task<IEnumerable<TodoItem>> FetchUncompletedAsync()
    {
        return await _repository.FetchUncompletedAsync();
    }

    public async Task<TodoItem?> FindByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            return null;

        return await _repository.FindByIdAsync(id);
    }

    public async Task<Guid> CreateAsync(TodoItem item)
    {
        item.EnsureNotNullOrDefault();

        var existing = await _repository.FindByDescriptionAndUncompletedAsync(item.Description);
        if (existing != null)
            throw new DuplicateNameException();

        return await _repository.CreateAsync(item);
    }

    public async Task UpdateAsync(TodoItem item)
    {
        item.EnsureNotNullOrDefault();

        await _repository.UpdateAsync(item);
    }
}
