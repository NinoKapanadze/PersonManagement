namespace PersonManagement.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public string Code { get; }
        public string Title { get; }

        protected AppException(string code, string title, string message)
            : base(message)
        {
            Code = code;
            Title = title;
        }
    }

}
