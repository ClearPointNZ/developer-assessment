using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Contexts
{
    public sealed class TodoContext : DbContext, ITodoContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) 
            : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        public void Create(TodoItem todo)
        {
            TodoItems.Add(todo);
        }

        public void Update(TodoItem todo)
        {
            base.Entry(todo).State = EntityState.Modified;
        }

        public void Delete(TodoItem todo)
        {
            base.Entry(todo).State = EntityState.Deleted;
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public Task<bool> DescriptionExists(string description)
        {
            return TodoItems.AnyAsync(
                item =>
                    item.Description.ToLowerInvariant() == description.ToLowerInvariant() &&
                    !item.IsCompleted);
        }

        public Task<TodoItem?> Get(Guid id)
        {
            return TodoItems.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
