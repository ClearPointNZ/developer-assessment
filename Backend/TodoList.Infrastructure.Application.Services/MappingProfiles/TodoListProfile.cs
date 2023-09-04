using Mapster;
using TodoList.Domain.Entities;
using TodoList.Services.Modules.TodoListModule.Models;
using TodoList.Services.Modules.TodoListModule.ViewModels;

namespace TodoList.Infrastructure.Persistence.EFCore.MappingProfiles
{
    public class TodoListProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateTodoItemModel, TodoItem>()
               .Map(dest => dest.Id, src => Guid.NewGuid())
               .Map(dest => dest.Description, src => src.Description)
               .Map(dest => dest.IsCompleted, src => false);


            config.ForType<TodoItem, GetTodoItemViewModel>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.Description, src => src.Description)
               .Map(dest => dest.IsCompleted, src => src.IsCompleted);
        }
    }
}
