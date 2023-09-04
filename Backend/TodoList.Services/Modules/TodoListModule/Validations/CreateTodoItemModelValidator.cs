using FluentValidation;
using TodoList.Services.Modules.TodoListModule.Models;

namespace TodoList.Services.Modules.TodoListModule.Validations
{
    public class CreateTodoItemModelValidator : AbstractValidator<CreateTodoItemModel>
    {
        public CreateTodoItemModelValidator()
        {
            RuleFor(_ => _.Description).NotEmpty().MaximumLength(100);
        }
    }
}
