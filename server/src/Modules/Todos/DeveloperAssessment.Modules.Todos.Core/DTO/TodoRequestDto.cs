using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeveloperAssessment.Modules.Todos.Core.DTO;

// Validation can be switched over to using FluentValidation
public sealed class TodoRequestDto
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 3)]
    public string? Description { get; set; } = string.Empty;

    [Required]
    public bool? IsCompleted { get; set; }
}
