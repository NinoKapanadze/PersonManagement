using PersonManagement.Application.Exceptions;

namespace PersonManagement.API.Middlewares
{
    /// <summary>
    /// Middleware for handling exceptions and returning standardized error responses.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger instance.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware logic for handling exceptions.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and writes an error response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception to handle.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An error occurred while processing the request.");

            var response = context.Response;
            response.ContentType = "application/json";

            var errorDetails = GetErrorDetails(exception);
            response.StatusCode = errorDetails.StatusCode;

            var errorResponse = new
            {
                error = new
                {
                    code = errorDetails.Code,
                    title = errorDetails.Title,
                    message = errorDetails.Message
                }
            };

            await response.WriteAsJsonAsync(errorResponse);
        }

        /// <summary>
        /// Gets the error details for the specified exception.
        /// </summary>
        /// <param name="exception">The exception to analyze.</param>
        /// <returns>A tuple containing status code, error code, title, and message.</returns>
        private (int StatusCode, string Code, string Title, string Message) GetErrorDetails(Exception exception)
        {
            return exception switch
            {
                NotFoundException ex => (404, ex.Code, ex.Title, ex.Message),
                ObjectAlreadyExistsException ex => (409, ex.Code, ex.Title, ex.Message),
                AppException ex => (400, ex.Code, ex.Title, ex.Message),
                _ => (500, "InternalServerError", "An unexpected error occurred", exception.Message)
            };
        }
    }
}
