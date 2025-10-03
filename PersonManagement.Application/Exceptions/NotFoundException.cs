namespace PersonManagement.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public string ResourceName { get; }

        public NotFoundException(string message, string resourceName)
            : base("NotFound", $"{resourceName} Not Found", message)
        {
            ResourceName = resourceName;
        }
    }

}
