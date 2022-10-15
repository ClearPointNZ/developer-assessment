using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Threading.Tasks;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService service)
        {
            _todoService = service;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var todos = await _todoService.GetAll();

            return Ok(todos);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id)
        {
            var todo = await _todoService.Get(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem todoItem)
        {
            /*
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemIdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            */
            throw new NotImplementedException();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)
        {
            /*
            if (string.IsNullOrEmpty(todoItem?.Description))
            {
                return BadRequest("Description is required");
            }
            else if (TodoItemDescriptionExists(todoItem.Description))
            {
                return BadRequest("Description already exists");
            } 

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
             
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            */
            throw new NotImplementedException();
        } 

        private bool TodoItemIdExists(Guid id)
        {
            /*
            return _context.TodoItems.Any(x => x.Id == id);
            */
            throw new NotImplementedException();
        }

        private bool TodoItemDescriptionExists(string description)
        {
            /*
            return _context.TodoItems
                   .Any(x => x.Description.ToLowerInvariant() == description.ToLowerInvariant() && !x.IsCompleted);
            */
            throw new NotImplementedException();
        }
    }
}
