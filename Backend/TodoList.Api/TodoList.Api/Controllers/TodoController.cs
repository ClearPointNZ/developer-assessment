using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using TodoApp.Exceptions;
using TodoApp.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService service)
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
        public async Task<IActionResult> GetTodoItem([FromRoute]Guid id)
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
        public async Task<IActionResult> PutTodoItem([FromRoute]Guid id, [FromBody]TodoBaseRequest request)
        {
            await _todoService.Update(new TodoItem { 
                Id = id, 
                Description = request.Description, 
                IsCompleted = request.IsCompleted });

            return Ok();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody]TodoRequest request)
        {   
            try
            {
                var todo = await _todoService.Create(
                    new TodoItem
                    {
                        Id = request.Id,
                        Description = request.Description.Trim(),
                        IsCompleted = request.IsCompleted
                    });

                return new OkObjectResult(todo) { StatusCode = (int)HttpStatusCode.Created };

            } catch(ExistingDescriptionException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/todoitems/{guid}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            await _todoService.Delete(id);

            return Ok();
        }
    }
}
