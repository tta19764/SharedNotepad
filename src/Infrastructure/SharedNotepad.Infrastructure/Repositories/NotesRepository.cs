using Microsoft.EntityFrameworkCore;
using SharedNotepad.Core.Models;
using SharedNotepad.Core.Repositories;
using SharedNotepad.Infrastructure.Data;

namespace SharedNotepad.Infrastructure.Repositories;

public class NotesRepository(AppDbContext context) : BaseRepository(context), INotesRepository
{
    private readonly DbSet<Note> _dbSet = context.Set<Note>();

    public async Task<Note?> GetAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Note> CreateAsync()
    {
        var now = DateTime.UtcNow;
        var note = new Note
        {
            CreatedAt = now,
            UpdatedAt = now
        };
        await _dbSet.AddAsync(note);
        await Context.SaveChangesAsync();
        return note;
    }

    public async Task UpdateAsync(Note note)
    {
        ArgumentNullException.ThrowIfNull(note);

        var existingNote = await _dbSet.FindAsync(note.Id);
        if (existingNote is null) return;

        existingNote.Text = note.Text;
        existingNote.UpdatedAt = DateTime.UtcNow;

        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var note = await _dbSet.FindAsync(id);
        if (note is null) return;

        _dbSet.Remove(note);
        await Context.SaveChangesAsync();
    }
}
