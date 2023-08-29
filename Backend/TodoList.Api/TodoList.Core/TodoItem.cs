namespace TodoList;

public class TodoItem
{
    public required Guid Id { get; set; }

    public required string Description { get; set; }

    public bool IsCompleted { get; set; } = false;
}
