using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BaseTemplate.API.Middleware;

/// <summary>
/// Global exception handling middleware for unhandled application exceptions.
/// Logs exceptions and returns standardized error responses.
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    /// <summary>Initializes a new instance of the GlobalExceptionHandlingMiddleware class.</summary>
    /// <param name="next">The next middleware in the pipeline</param>
    /// <param name="logger">Logger instance for exception logging</param>
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>Invokes the middleware to handle exceptions.</summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>Handles exception and writes response.</summary>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = "An internal server error occurred.",
            details = exception.Message
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
