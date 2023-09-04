using System.Collections.Generic;

namespace TodoList.Api.Models
{
    public class ApiErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Status { get; set; }
        public string Instance { get; set; }
        public IEnumerable<Dictionary<string, string[]>> Errors { get; set; }
    }
}
