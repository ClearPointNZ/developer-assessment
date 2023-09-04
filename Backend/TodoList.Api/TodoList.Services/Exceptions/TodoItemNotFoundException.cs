namespace TodoList.Services.Exceptions
{
    public class TodoItemNotFoundException : Exception
    {
        public TodoItemNotFoundException()
        {
        }

        public TodoItemNotFoundException(string? message) : base(message)
        {
        }

        public TodoItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
