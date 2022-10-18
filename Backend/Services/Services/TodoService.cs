using Core.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Contexts;
using TodoApp.Exceptions;
using TodoApp.Extensions;

namespace Services.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoContext _context;

        public TodoService(ITodoContext context)
        {
            _context = context;
        }

        /// <exception cref="ExistingDescriptionException">If an item exists with the same description and is active</exception>
        public async Task<TodoItem> Create(TodoItem item)
        {
            item.EnsureNotNull();

            item.Description.EnsureNotNullOrWhiteSpace();

            if (await _context.DescriptionExists(item.Description)) 
            {
                throw new ExistingDescriptionException();
            }

            _context.Create(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task Delete(Guid id)
        {
            var item = await Get(id);          

            if(item != null)
            {
                _context.Delete(item);

                await _context.SaveChangesAsync();
            }
        }

        public Task<List<TodoItem>> GetAll()
        {
            // TODO consider pagination
            return _context
                .TodoItems
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<TodoItem?> Get(Guid id)
        {
            return _context.Get(id);
        }

        public async Task Update(TodoItem item)
        {
            var existingTodo = await Get(item.Id);

            if(existingTodo != null)
            {
                existingTodo.Description = item.Description;
                existingTodo.IsCompleted = item.IsCompleted;

                _context.Update(item);

                await _context.SaveChangesAsync();
            }
        }
    }
}
