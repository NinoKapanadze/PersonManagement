namespace PersonManagement.Application.Exceptions
{
    public class ObjectAlreadyExistsException : AppException
    {
        public ObjectAlreadyExistsException(string message)
            : base("AlreadyExists", "Object Already Exists", message)
        {
        }
    }
}
