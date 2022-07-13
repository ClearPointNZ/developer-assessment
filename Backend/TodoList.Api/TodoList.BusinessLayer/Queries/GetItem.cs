using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Data.Models;
using X.PagedList;

namespace TodoList.BusinessLayer.Queries
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public record GetItem(Guid Id) : IRequest<TodoItem?>
    {
        private class GetItemHandler : IRequestHandler<GetItem, TodoItem?>
        {
            private readonly TodoContext _dbContext;

            public GetItemHandler(TodoContext dbContext) => _dbContext = dbContext;

            public async Task<TodoItem?> Handle(GetItem request, CancellationToken cancellationToken)
            {
                return await _dbContext.TodoItems.Where(t => t.Id == request.Id).SingleOrDefaultAsync(cancellationToken); 
            }
        }
    }
}
