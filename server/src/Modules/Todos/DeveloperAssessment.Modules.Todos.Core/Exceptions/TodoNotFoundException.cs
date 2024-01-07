using DeveloperAssessment.Shared.Exceptions;

namespace DeveloperAssessment.Modules.Todos.Core.Exceptions;

internal sealed class TodoNotFoundException(Guid todoId) : DeveloperAssessmentException($"Todo with ID: '{todoId}' was not found.");
