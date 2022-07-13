using FluentValidation;
using MediatR;
using TodoList.BusinessLayer.Queries;
using TodoList.Data;
using TodoList.Data.Models;

namespace TodoList.BusinessLayer.Commands;

public record CreateTodoItem(string Description, bool IsComplete) : IRequest<Guid>
{
    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItem, Guid>
    {
        private readonly TodoContext _dbContext;

        public CreateTodoItemHandler(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateTodoItem request, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem()
            {
                Description = request.Description,
                IsCompleted = request.IsComplete
            };
            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return todoItem.Id;
        }
        
       
    }
}
