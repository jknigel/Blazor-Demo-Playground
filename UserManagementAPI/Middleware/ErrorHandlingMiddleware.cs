using System.Net;
using System.Text.Json;

namespace UserManagementAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Try to pass the request down the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // If any middleware after this one throws an exception, we catch it here.
            _logger.LogError(ex, "An unhandled exception has occurred.");

            // Create a consistent JSON error response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new { error = "An internal server error occurred. Please try again later." };
            var jsonResponse = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}