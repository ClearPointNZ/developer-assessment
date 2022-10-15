using Core.Models;

namespace Services.Services
{
    public interface ITodoService
    {
        Task<TodoItem?> Get(Guid id);

        Task<IList<TodoItem>> GetAll();

        Task Create(TodoItem item);

        Task Update(TodoItem item);

        Task Delete(Guid id);
    }

}