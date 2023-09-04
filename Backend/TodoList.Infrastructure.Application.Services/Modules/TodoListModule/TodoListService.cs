using Mapster;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;
using TodoList.Services.Modules.TodoListModule;
using TodoList.Services.Modules.TodoListModule.Models;
using TodoList.Services.Modules.TodoListModule.ViewModels;
using TodoList.Services.Shared;

namespace TodoList.Infrastructure.Application.Services.Modules.TodoListModule
{
    internal class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository todoListRepository;
        public TodoListService(ITodoListRepository todoListRepository)
        {
            this.todoListRepository = todoListRepository;
        }
        public async Task<ServiceResult<GetTodoItemViewModel>> CreateTodoItem(CreateTodoItemModel item, CancellationToken cancellationToken)
        {
            var result = new ServiceResult<GetTodoItemViewModel>();
            if (item == null)
            {
                result.BadRequestWithMessage("Description is required");
                return result;
            }

            var newItem = await this.todoListRepository.CreateTodoItem(item.Adapt<TodoItem>(), cancellationToken).ConfigureAwait(false);
            if (newItem != null)
            {
                return result.ToResourceCreatedResult(newItem.Adapt<GetTodoItemViewModel>());
            }

            result.WithResultCode(ResultCode.InternalServerError, "An unknown error occurred while creating TodoList Item");
            return result;
        }

        public async Task<ServiceResult<GetTodoItemViewModel>> GetItem(Guid id, CancellationToken cancellationToken)
        {
            var result = new ServiceResult<GetTodoItemViewModel>();
            var item = await this.todoListRepository.GetTodoItem(id, cancellationToken).ConfigureAwait(false);
            if (item == null)
            {
                result.WithResultCode(ResultCode.NotFound, $"Item ({id}) not found");
                return result;
            }

            return result.ToSuccessResult(item.Adapt<GetTodoItemViewModel>());
        }

        public async Task<IEnumerable<GetTodoItemViewModel>> GetItems(CancellationToken token)
        {
            var items= await this.todoListRepository.GetTodoItems(token).ConfigureAwait(false);
            return items.Adapt<List<GetTodoItemViewModel>>();
        }

        public async Task<ServiceResult> UpdateTodoItemStatus(Guid id, UpdateTodoItemModel item, CancellationToken cancellationToken)
        {
            var result = new ServiceResult();
            var originalItem = await this.todoListRepository.GetTodoItem(id, cancellationToken).ConfigureAwait(false);
            if (originalItem == null)
            {
                result.WithResultCode(ResultCode.NotFound, $"Item ({id}) not found");
                return result;
            }

            originalItem.IsCompleted = item.IsCompleted;

            var isUpdated = await this.todoListRepository.UpdateTodoItem(originalItem, cancellationToken).ConfigureAwait(false);

            return result.WithResultCode(isUpdated ? ResultCode.Updated : ResultCode.InternalServerError);
        }
    }
}
