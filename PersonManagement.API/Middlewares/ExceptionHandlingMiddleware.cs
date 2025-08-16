using FluentValidation;
using PersonManagement.Application.Exceptions;

namespace PersonManagement.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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

        private (int StatusCode, string Code, string Title, string Message) GetErrorDetails(Exception exception)
        {
            return exception switch
            {
                NotFoundException ex => (404, ex.Code, ex.Title, ex.Message),
                ObjectAlreadyExistsException ex => (409, ex.Code, ex.Title, ex.Message),
                ValidationException ex => (400, "ValidationError", "Validation Error",
                string.Join(", ", ex.Errors.Select(e => e.ErrorMessage))),
                AppException ex => (400, ex.Code, ex.Title, ex.Message),
                _ => (500, "InternalServerError", "An unexpected error occurred", exception.Message)
            };
        }
    }
}
