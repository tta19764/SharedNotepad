namespace Backend.DTOs;

public sealed record NoteDto(
    Guid Id,
    string Text
);