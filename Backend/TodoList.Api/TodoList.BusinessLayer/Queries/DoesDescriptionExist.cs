using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;

namespace TodoList.BusinessLayer.Queries
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public record DoesDescriptionExist(string Description) : IRequest<bool>
    {
        private class DoesDescriptionExistHandler : IRequestHandler<DoesDescriptionExist, bool>
        {
            private readonly TodoContext _dbContext;

            public DoesDescriptionExistHandler(TodoContext dbContext) => _dbContext = dbContext;

            public async Task<bool> Handle(DoesDescriptionExist request, CancellationToken cancellationToken)
            {
                return await _dbContext.TodoItems.AnyAsync(t => string.Equals(t.Description, request.Description, StringComparison.CurrentCultureIgnoreCase), cancellationToken); 
            }
        }
    }
}
