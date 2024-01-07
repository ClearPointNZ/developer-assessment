namespace DeveloperAssessment.Modules.Todos.Core.DTO;

public sealed class TodoResponseDto
{
    public required Guid Id { get; set; }

    public required string Description { get; set; }

    public required bool IsCompleted { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}
