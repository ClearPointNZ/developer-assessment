using DeveloperAssessment.Modules.Todos.Core.DTO;
using DeveloperAssessment.Modules.Todos.Core.Entities;

namespace DeveloperAssessment.Modules.Todos.Core.Mappings;

// Mapping between DTO and Entity
// Can be swapped out for AutoMapper
internal static class Extensions
{
    public static Todo ToEntity(this TodoRequestDto dto) => new(dto.Id, dto.Description!, dto.IsCompleted ?? false);

    public static TodoResponseDto ToDto(this Todo entity) => new()
    {
        Id = entity.Id,
        Description = entity.Description,
        IsCompleted = entity.IsCompleted,
        CreatedAt = entity.CreatedAt,
        CompletedAt = entity.CompletedAt
    };
}
