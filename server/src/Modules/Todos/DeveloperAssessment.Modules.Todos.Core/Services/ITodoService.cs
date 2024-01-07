using DeveloperAssessment.Modules.Todos.Core.DTO;

namespace DeveloperAssessment.Modules.Todos.Core.Services;

public interface ITodoService
{
    Task<TodoResponseDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<TodoResponseDto>> GetAllAsync(bool showCompleted);
    Task<TodoResponseDto> AddAsync(TodoRequestDto todo);
    Task UpdateAsync(TodoRequestDto todo);
}
