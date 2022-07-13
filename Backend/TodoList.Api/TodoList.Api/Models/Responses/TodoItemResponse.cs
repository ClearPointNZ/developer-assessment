using System;

namespace TodoList.Api.Models.Responses;

public class TodoItemResponse
{
    public Guid Id { get; set; }

    public string Description { get; set; }
}