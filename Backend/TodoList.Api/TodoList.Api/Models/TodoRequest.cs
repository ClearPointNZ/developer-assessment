using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public record TodoRequest
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
