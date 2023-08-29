namespace TodoList;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> FetchUncompletedAsync();
    Task<TodoItem?> FindByIdAsync(Guid id);

    Task<Guid> CreateAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
}
