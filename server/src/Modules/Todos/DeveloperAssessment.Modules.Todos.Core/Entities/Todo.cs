namespace DeveloperAssessment.Modules.Todos.Core.Entities;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

public sealed class Todo(Guid id, string description, bool isCompleted)
{
    public Guid Id { get; private set; } = id;

    public string Description { get; private set; } = description;

    public bool IsCompleted { get; private set; } = isCompleted;

    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; private set; }

    public bool TryUpdate(string description, bool isCompleted) => UpdateDescriptionIfChanged(description) || UpdateIsCompletedIfChanged(isCompleted);

    private bool UpdateDescriptionIfChanged(string description)
    {
        if (Description == description)
        {
            return false;
        }

        Description = description;

        return true;
    }

    private bool UpdateIsCompletedIfChanged(bool isCompleted)
    {
        if (IsCompleted == isCompleted)
        {
            return false;
        }

        IsCompleted = isCompleted;
        CompletedAt = isCompleted ? DateTime.UtcNow : null;

        return true;
    }
}
