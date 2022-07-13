using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;
using TodoList.BusinessLayer.Commands;
using TodoList.BusinessLayer.Queries;
using TodoList.Data.Models;
using X.PagedList;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ISender _sender;        
        private readonly IMapper _mapper;
        
        public TodoItemsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<List<TodoItemResponse>> GetTodoItems()
        {
            var result = await _sender.Send(new GetTodoList());
            return _mapper.Map<List<TodoItemResponse>>(result);
        }

        // GET: api/TodoItems/...
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TodoItemResponse>> GetTodoItem(Guid id)
        {
            var result = await _sender.Send(new GetItem(id));

            if (result == null)
            {
                return NotFound();
            }

            return _mapper.Map<TodoItemResponse>(result);
        }

        // PUT: api/TodoItems/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, UpdateTodoItemRequest todoItem)
        {
            if (await _sender.Send(new DoesItemExist(id)) == false)
            {
                return NotFound();
            }

            await _sender.Send(new UpdateTodoItem(id, todoItem.Description, todoItem.IsCompleted));
            return NoContent();
        } 

        // POST: api/TodoItems 
        [HttpPost]
        public async Task<ActionResult> PostTodoItem(CreateTodoItemRequest todoItem)
        {
            var createItemTodo = new CreateTodoItem(todoItem.Description, todoItem.IsCompleted);
            var createdTodoItemId = await _sender.Send(createItemTodo);
             
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItemId }, createdTodoItemId);
        }
    }
}
