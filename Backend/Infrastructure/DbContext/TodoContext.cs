using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Contexts
{
    public class TodoContext : DbContext, ITodoContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) 
            : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        public void Create(TodoItem todo)
        {
            TodoItems.Add(todo);
        }

        public void Delete(TodoItem todo)
        {
            base.Entry(todo).State = EntityState.Deleted;
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public void Update(TodoItem todo)
        {
            base.Entry(todo).State = EntityState.Modified;
        }
    }
}
