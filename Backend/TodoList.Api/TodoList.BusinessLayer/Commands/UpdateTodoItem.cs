using System.Net;
using FluentValidation;
using MediatR;
using TodoList.BusinessLayer.Queries;
using TodoList.Data;

namespace TodoList.BusinessLayer.Commands;

public sealed record UpdateTodoItem(Guid Id, string Description, bool IsComplete) : IRequest
{

    public sealed class UpdateTodoItemHandler : AsyncRequestHandler<UpdateTodoItem>
    {
        private readonly TodoContext _dbContext;
        private readonly ISender _sender;

        public UpdateTodoItemHandler(TodoContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }

        protected override async Task  Handle(UpdateTodoItem request, CancellationToken cancellationToken)
        {
            var todoItem = await _sender.Send(new GetItem(request.Id), cancellationToken);
            if (todoItem == null)
            {
                throw new InvalidOperationException($"{request.Id} is not a valid Todo Item Id.");
            }

            todoItem.Update(request.Description, request.IsComplete);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
