using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;

namespace TodoList.Infrastructure.Persistence.EFCore.Repositories
{
    internal class TodoListRepository : ITodoListRepository
    {
        private readonly TodoListContext todoListContext;
        public TodoListRepository(TodoListContext todoListContext)
        {
            this.todoListContext = todoListContext;
        }

        public async Task<TodoItem> CreateTodoItem(TodoItem item, CancellationToken token)
        {
            await this.todoListContext.TodoItems.AddAsync(item, token).ConfigureAwait(false);
            await this.todoListContext.SaveChangesAsync(token).ConfigureAwait(false);
            return item;
        }

        public async Task<TodoItem> GetTodoItem(Guid itemId, CancellationToken token)
        {
            return await this.todoListContext.FindAsync<TodoItem>(itemId, token).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(CancellationToken token)
        {
            return await this.todoListContext.TodoItems.ToArrayAsync(token).ConfigureAwait(false);
        }

        public async Task<bool> UpdateTodoItem(TodoItem item, CancellationToken token)
        {
            this.todoListContext.Update<TodoItem>(item);
            await this.todoListContext.SaveChangesAsync(token).ConfigureAwait(false);
            return true;
        }
    }
}
