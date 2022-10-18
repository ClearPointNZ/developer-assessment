using Core.Models;

namespace Services.Services
{
    public interface ITodoService
    {
        Task<TodoItem?> Get(Guid id);

        Task<List<TodoItem>> GetAll();

        Task<TodoItem> Create(TodoItem item);

        Task Update(TodoItem item);

        Task Delete(Guid id);
    }

}