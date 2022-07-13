using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using X.PagedList;

namespace TodoList.BusinessLayer.Queries
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public record DoesItemExist(Guid Id) : IRequest<bool>
    {
        private class DoesItemExistHandler : IRequestHandler<DoesItemExist, bool>
        {
            private readonly TodoContext _dbContext;

            public DoesItemExistHandler(TodoContext dbContext) => _dbContext = dbContext;

            public async Task<bool> Handle(DoesItemExist request, CancellationToken cancellationToken)
            {
                return await _dbContext.TodoItems.AnyAsync(t => t.Id == request.Id, cancellationToken); 
            }
        }
    }
}
