using JelloBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace JelloBackend.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(){}

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }


    public DbSet<Board> Boards { get; set; } = null!;

    public DbSet<Column> Columns { get; set; } = null!;

    public DbSet<Element> Elements { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost:5432;Database=JelloDB;Username=postgres;Password=-cdNq:6S:9H2E6n");
    }
}