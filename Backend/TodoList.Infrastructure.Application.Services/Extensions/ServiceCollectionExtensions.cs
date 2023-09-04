using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infrastructure.Application.Services.Modules.TodoListModule;
using TodoList.Services.Modules.TodoListModule;
using TodoList.Services.Modules.TodoListModule.Validations;

namespace TodoList.Infrastructure.Application.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddValidation();
            services.AddTransient<ITodoListService, TodoListService>();
        }

        private static void AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(_ =>
            {
                _.DisableDataAnnotationsValidation = true;
            });
            services.AddValidatorsFromAssemblyContaining<CreateTodoItemModelValidator>();
        }
    }
}