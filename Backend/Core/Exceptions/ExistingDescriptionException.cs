namespace TodoApp.Exceptions
{
    public class ExistingDescriptionException : Exception
    {
        const string DefaultMessage = "The Todo description already exists";
        public ExistingDescriptionException() : base(DefaultMessage) {}
        public ExistingDescriptionException(string message) : base(message) {}
        public ExistingDescriptionException(string message, Exception inner) : base(message, inner) { }
    }
}
