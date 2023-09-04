using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public interface ITodoListRepository
    {
        Task<TodoItem> CreateTodoItem(TodoItem item, CancellationToken token);
        Task<bool> UpdateTodoItem(TodoItem item, CancellationToken token);
        Task<TodoItem> GetTodoItem(Guid itemId, CancellationToken token);
        Task<IEnumerable<TodoItem>> GetTodoItems(CancellationToken token);
    }
}
