namespace TodoList.Data.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public void Update(string description, bool isCompleted)
        {
            this.Description = description;
            this.IsCompleted = isCompleted;
        }
    }
}
