using System;

namespace TodoList.Testing.Generators
{
    internal record TodoItemGenerator
    {
        public static TodoItem Create()
        {
            Random random = new();

            return new TodoItem
            {
                Id = Guid.NewGuid(),
                Description = $"Description-{Guid.NewGuid().ToString().Substring(3)}",
                IsCompleted = random.Next(1000) % 2 == 0
            };
        }
    }
}