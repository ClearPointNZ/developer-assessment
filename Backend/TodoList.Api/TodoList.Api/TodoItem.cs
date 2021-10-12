using System;

namespace TodoList.Api
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
