using SharedNotepad.Infrastructure.Data;

namespace SharedNotepad.Infrastructure.Repositories;

public abstract class BaseRepository (AppDbContext context)
{
    protected AppDbContext Context { get; } = context;
}