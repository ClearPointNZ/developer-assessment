using System.Runtime.CompilerServices;
using DeveloperAssessment.Modules.Todos.Core.DAL.Repositories;
using DeveloperAssessment.Modules.Todos.Core.DTO;
using DeveloperAssessment.Modules.Todos.Core.Exceptions;
using DeveloperAssessment.Modules.Todos.Core.Mappings;
using Microsoft.Extensions.Logging;

[assembly:InternalsVisibleTo("DeveloperAssessment.Modules.Todos.Core.Tests")]
[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DeveloperAssessment.Modules.Todos.Core.Services;

internal sealed class TodoService(ITodoRepository todoRepository, ILogger<TodoService> logger) : ITodoService
{
    public async Task<TodoResponseDto?> GetByIdAsync(Guid id)
    {
        var todo = await todoRepository.GetByIdAsync(id);

        return todo?.ToDto();
    }

    public async Task<IReadOnlyList<TodoResponseDto>> GetAllAsync(bool showCompleted)
    {
        var todos = await todoRepository.GetAllAsync(showCompleted);

        return todos.Select(todo => todo.ToDto()).ToList();
    }

    public async Task<TodoResponseDto> AddAsync(TodoRequestDto dto)
    {
        dto.Id = Guid.NewGuid();

        var newTodo = await todoRepository.AddAsync(dto.ToEntity());

        logger.LogInformation("Created a todo with ID: '{Id}'", newTodo.Id);

        return newTodo.ToDto();
    }

    public async Task UpdateAsync(TodoRequestDto dto)
    {
        var todo = await todoRepository.GetByIdAsync(dto.Id) ?? throw new TodoNotFoundException(dto.Id);
        var isTodoUpdated = todo.TryUpdate(dto.Description!, dto.IsCompleted ?? false);

        if (!isTodoUpdated)
        {
            // No changes so just return
            return;
        }

        await todoRepository.UpdateAsync(todo);

        logger.LogInformation("Updated a todo with ID: '{Id}'", dto.Id);
    }
}
