using Microsoft.EntityFrameworkCore;
using SharedNotepad.Core.Models;

namespace SharedNotepad.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Note>().Property(x => x.Id).ValueGeneratedOnAdd();
    }
}