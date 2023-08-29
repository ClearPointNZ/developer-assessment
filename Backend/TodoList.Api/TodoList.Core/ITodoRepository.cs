namespace TodoList;

public interface ITodoRepository
{
    Task<IList<TodoItem>> FetchAllAsync();
    Task<IList<TodoItem>> FetchUncompletedAsync();

    Task<TodoItem?> FindByIdAsync(Guid id);
    Task<TodoItem?> FindByDescriptionAndUncompletedAsync(string description);

    Task<Guid> CreateAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
    Task DeleteAsync(Guid id);
}
