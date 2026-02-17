namespace Backend.AppExtensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiExplorer(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseOpenApiExplorer(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
