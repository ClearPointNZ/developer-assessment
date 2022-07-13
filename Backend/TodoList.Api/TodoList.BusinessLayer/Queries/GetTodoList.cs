using System.Diagnostics.CodeAnalysis;
using MediatR;
using TodoList.Data;
using TodoList.Data.Models;
using X.PagedList;

namespace TodoList.BusinessLayer.Queries
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public record GetTodoList : IRequest<IPagedList<TodoItem>>
    {

        public int Page { get; } = 1;
        public int PageSize { get; } = 10;

        private class GetTodoListHandler : IRequestHandler<GetTodoList, IPagedList<TodoItem>>
        {
            private readonly TodoContext _dbContext;

            public GetTodoListHandler(TodoContext dbContext) => _dbContext = dbContext;

            public async Task<IPagedList<TodoItem>> Handle(GetTodoList request, CancellationToken cancellationToken)
            {
                return await _dbContext.TodoItems.Where(x => !x.IsCompleted).ToPagedListAsync(request.Page,request.PageSize,cancellationToken);
            }
        }
    }
}
