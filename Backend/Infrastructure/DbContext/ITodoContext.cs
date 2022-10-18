using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Contexts
{
    public interface ITodoContext : IDisposable
    {
        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync();

        void Create(TodoItem todo);
        
        void Update(TodoItem todo);

        void Delete(TodoItem todo);

        Task<bool> DescriptionExists(string description);

        Task<TodoItem?> Get(Guid id);
    }
}
