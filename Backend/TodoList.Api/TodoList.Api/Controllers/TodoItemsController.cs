using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoList.Services.Modules.TodoListModule;
using TodoList.Services.Modules.TodoListModule.Models;
using TodoList.Services.Shared;

namespace TodoList.Api.Controllers
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TodoItemsController : ApiControllerBase
    {
        private readonly ILogger<TodoItemsController> logger;
        private readonly ITodoListService todoListService;

        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoListService todoListService)
        {
            this.logger = logger;
            this.todoListService = todoListService;
        }

        // GET: api/TodoItems/...
        [HttpGet]
        public async Task<IActionResult> GetTodoItem(CancellationToken token)
        {
            var result = await this.todoListService.GetItems(token).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(Guid id, CancellationToken token)
        {
            var result = await this.todoListService.GetItem(id, token).ConfigureAwait(false);

            if (result.ResultCode == ResultCode.NotFound)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(CreateTodoItemModel todoItem, CancellationToken token)
        {
            var result = await this.todoListService.CreateTodoItem(todoItem, token).ConfigureAwait(false);
            if (result.ResultCode == ResultCode.Created)
            {
                return CreatedAtAction(nameof(GetTodoItem), new { id = result.Result.Id }, result.Result);
            }

            return BadRequest(result.Errors);

        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, UpdateTodoItemModel todoItem, CancellationToken token)
        {
            var result = await this.todoListService.UpdateTodoItemStatus(id, todoItem, token).ConfigureAwait(false);

            if (result.ResultCode == ResultCode.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
