using Microsoft.EntityFrameworkCore;
using SharedNotepad.Infrastructure.Data;

namespace Backend.AppExtensions;

public static class DatabaseMigrationExtensions
{
    public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        return app;
    }
}
