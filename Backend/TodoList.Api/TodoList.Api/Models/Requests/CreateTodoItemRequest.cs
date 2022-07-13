namespace TodoList.Api.Models.Requests;

public record CreateTodoItemRequest
{
    public string Description { get; init; }

    public bool IsCompleted { get; init; }
}