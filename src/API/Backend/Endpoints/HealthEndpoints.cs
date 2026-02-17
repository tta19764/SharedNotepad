using Microsoft.EntityFrameworkCore;
using SharedNotepad.Infrastructure.Data;

namespace Backend.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", async (AppDbContext dbContext, CancellationToken cancellationToken) =>
        {
            var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

            return canConnect
                ? Results.Ok(new { status = "Healthy" })
                : Results.Problem(
                    title: "Unhealthy",
                    detail: "Database connection check failed.",
                    statusCode: StatusCodes.Status503ServiceUnavailable);
        })
        .WithName("Health")
        .WithTags("Health");

        return app;
    }
}
