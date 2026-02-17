using Backend.Endpoints;
using Backend.AppExtensions;
using Backend.ExceptionHandling;
using Backend.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddCors(options =>
    {
        options.AddPolicy("FrontendDev", policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    })
    .AddPostgresDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddOpenApiExplorer();

var app = builder.Build();

app.ApplyDatabaseMigrations();

app.UseExceptionHandler();
app.UseCors("FrontendDev");
app.UseOpenApiExplorer();

app.MapHealthEndpoints();
app.MapNotesEndpoints();

app.Run();
