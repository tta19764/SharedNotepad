using SharedNotepad.Core.Models;

namespace SharedNotepad.Core.Repositories;

public interface INotesRepository
{
    Task<Note?> GetAsync(Guid id);
    Task<Note> CreateAsync();
    Task UpdateAsync(Note note);
    Task DeleteAsync(Guid id);
}