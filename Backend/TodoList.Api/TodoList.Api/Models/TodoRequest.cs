using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public record TodoRequest : TodoBaseRequest
    {
        public Guid Id { get; set; }
    }

    public record TodoBaseRequest
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
