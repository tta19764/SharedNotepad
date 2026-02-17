using Backend.DTOs;
using SharedNotepad.Core.Models;
using SharedNotepad.Core.Repositories;

namespace Backend.Endpoints;

public static class NotesEndpoints
{
    public static IEndpointRouteBuilder MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/notes");

        group.MapGet("/{id:guid}", async (Guid id, INotesRepository repository) =>
        {
            var note = await repository.GetAsync(id);
            return note is null
                ? Results.NotFound()
                : Results.Ok(ToDto(note));
        })
        .WithName("GetNoteById");

        group.MapPost(string.Empty, async (INotesRepository repository) =>
        {
            var note = await repository.CreateAsync();
            var response = new CreateNoteResponse(note.Id);
            return Results.Created($"/notes/{note.Id}", response);
        })
        .WithName("CreateNote");

        group.MapPut("/{id:guid}", async (Guid id, UpdateNoteRequest request, INotesRepository repository) =>
        {
            var existing = await repository.GetAsync(id);
            if (existing is null)
            {
                return Results.NotFound();
            }

            existing.Text = request.Text;
            await repository.UpdateAsync(existing);
            return Results.NoContent();
        })
        .WithName("UpdateNote");

        group.MapDelete("/{id:guid}", async (Guid id, INotesRepository repository) =>
        {
            await repository.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteNote");

        return app;
    }

    private static NoteDto ToDto(Note note) => new(note.Id, note.Text);
}
