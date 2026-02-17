using Microsoft.EntityFrameworkCore;
using SharedNotepad.Core.Repositories;
using SharedNotepad.Infrastructure.Data;
using SharedNotepad.Infrastructure.Repositories;

namespace Backend.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<INotesRepository, NotesRepository>();
        return services;
    }

    public static IServiceCollection AddPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}
