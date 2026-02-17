using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Backend.ExceptionHandling;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception for request {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var payload = new
        {
            title = "An unexpected error occurred.",
            status = StatusCodes.Status500InternalServerError,
            traceId = httpContext.TraceIdentifier
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(payload), cancellationToken);
        return true;
    }
}
