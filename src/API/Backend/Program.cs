using Backend.Endpoints;
using Backend.AppExtensions;
using Backend.ExceptionHandling;
using Backend.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddPostgresDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddOpenApiExplorer();

var app = builder.Build();

app.ApplyDatabaseMigrations();

app.UseExceptionHandler();
app.UseOpenApiExplorer();

app.MapNotesEndpoints();

app.Run();
