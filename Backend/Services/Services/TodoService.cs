using Core.Models;
using Microsoft.EntityFrameworkCore;
using TodoApp.Contexts;

namespace Services.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoContext _context;

        public TodoService(ITodoContext context)
        {
            _context = context;
        }

        public async Task Create(TodoItem item)
        {
            _context.Create(item);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public async Task Delete(Guid id)
        {
            var item = await Get(id);
            
            if(item == null) { throw new Exception(""); }

            _context.Delete(item);

            await _context.SaveChangesAsync();
        }

        public async Task<IList<TodoItem>> GetAll()
        {
            // TODO consider pagination
            return await _context
                .TodoItems
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TodoItem?> Get(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
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
