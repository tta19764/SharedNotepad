namespace SharedNotepad.Core.Models;

public sealed class Note : BaseModel
{
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}