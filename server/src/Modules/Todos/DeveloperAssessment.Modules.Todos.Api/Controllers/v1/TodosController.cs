using System.Runtime.CompilerServices;
using Asp.Versioning;
using DeveloperAssessment.Modules.Todos.Core.DTO;
using DeveloperAssessment.Modules.Todos.Core.Services;
using Microsoft.AspNetCore.Mvc;

[assembly:InternalsVisibleTo("DeveloperAssessment.Modules.Todos.Api.Tests")]
namespace DeveloperAssessment.Modules.Todos.Api.Controllers.v1;

[ApiVersion("1.0")]
internal sealed class TodosController(ITodoService todoService) : BaseController
{
    /// <summary>
    /// Get Todo by ID
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    /// <returns>The todo item if found, or null if not found.</returns>
    /// <response code="200">Returns the todo item.</response>
    /// <response code="404">If the todo item is not found.</response>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoResponseDto>> GetById(Guid id)
    {
        var todo = await todoService.GetByIdAsync(id);

        return todo is null ? NotFound() : Ok(todo);
    }

    /// <summary>
    /// Get all Todos
    /// </summary>
    /// <param name="showCompleted">Flag to indicate whether to show completed todos or not.</param>
    /// <returns>A list of todo items.</returns>
    /// <response code="200">Returns a list of todo items.</response>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoResponseDto>>> GetAll([FromQuery] bool showCompleted = false)
    {
        var todos = await todoService.GetAllAsync(showCompleted);

        return Ok(todos);
    }

    /// <summary>
    /// Create a new Todo
    /// </summary>
    /// <param name="dto">The TodoRequestDto object containing the details of the new todo item.</param>
    /// <returns>The created todo item.</returns>
    /// <response code="201">Returns the created todo item.</response>
    [HttpPost]
    public async Task<ActionResult> Post(TodoRequestDto dto)
    {
        var newTodo = await todoService.AddAsync(dto);

        return CreatedAtAction(nameof(GetById), new { newTodo.Id }, newTodo);
    }

    /// <summary>
    /// Update a Todo
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    /// <param name="dto">The TodoRequestDto object containing the updated details of the todo item.</param>
    /// <returns>No content.</returns>
    /// <response code="204">Indicates that the todo item was successfully updated.</response>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, TodoRequestDto dto)
    {
        dto.Id = id;

        await todoService.UpdateAsync(dto);

        return NoContent();
    }
}
