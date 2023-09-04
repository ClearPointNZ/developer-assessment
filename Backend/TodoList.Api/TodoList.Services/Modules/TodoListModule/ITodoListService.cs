using TodoList.Services.Modules.TodoListModule.Models;
using TodoList.Services.Modules.TodoListModule.ViewModels;
using TodoList.Services.Shared;

namespace TodoList.Services.Modules.TodoListModule
{
    public interface ITodoListService
    {
        Task<ServiceResult<GetTodoItemViewModel>> CreateTodoItem(CreateTodoItemModel item, CancellationToken cancellationToken);
        Task<ServiceResult> UpdateTodoItemStatus(Guid id, UpdateTodoItemModel item, CancellationToken cancellationToken);
        Task<ServiceResult<GetTodoItemViewModel>> GetItem(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<GetTodoItemViewModel>> GetItems(CancellationToken token);
    }
}
