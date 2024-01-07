using DeveloperAssessment.Modules.Todos.Core.Entities;

namespace DeveloperAssessment.Modules.Todos.Core.DAL.Repositories;

public interface ITodoRepository
{
    Task<Todo?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Todo>> GetAllAsync(bool showCompleted);
    Task<Todo> AddAsync(Todo todo);
    Task UpdateAsync(Todo todo);
}
