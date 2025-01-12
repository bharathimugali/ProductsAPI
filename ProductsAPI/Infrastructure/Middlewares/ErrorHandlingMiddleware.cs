using System.ComponentModel.DataAnnotations;
using ProductsAPI.Infrastructure.Exceptions;

namespace ProductsAPI.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                NotFoundException _ => StatusCodes.Status404NotFound,
                ValidationException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError // Default to 500 for unexpected exceptions
            };

            var response = new { message = exception.Message };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
