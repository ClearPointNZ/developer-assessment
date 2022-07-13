using FluentValidation;
using MediatR;
using TodoList.Api.Models.Requests;
using TodoList.BusinessLayer.Queries;

namespace TodoList.Api.Validators;

public sealed class CreateTodoItemValidator : AbstractValidator<CreateTodoItemRequest>
{
    public CreateTodoItemValidator(ISender sender)
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithSeverity(Severity.Info)
            .WithMessage("Description is required")
            .WithErrorCode("DescriptionIsRequired");

        RuleFor(x => x.Description)
            .Must(description => sender.Send(new DoesDescriptionExist(description)).Result == false)
            .WithSeverity(Severity.Info)
            .WithMessage("Description already exists")
            .WithErrorCode("DuplicatedDescription");
    }
    
}