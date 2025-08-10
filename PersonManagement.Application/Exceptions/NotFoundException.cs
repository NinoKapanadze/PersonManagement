namespace PersonManagement.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base("NotFound", "Resource Not Found", message)
        {
        }
    }

}
