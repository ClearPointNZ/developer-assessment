using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Contexts
{
    public interface ITodoContext : IDisposable
    {
        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync();

        void Create(TodoItem todo);
        
        void Update(TodoItem todo);

        void Delete(TodoItem todo);
    }
}
