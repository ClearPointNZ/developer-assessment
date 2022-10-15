﻿namespace Core.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; } = default!;

        public bool IsCompleted { get; set; }
    }
}