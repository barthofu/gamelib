using gamelib.Models;
using Microsoft.EntityFrameworkCore;

namespace gamelib.Context;

public class GamelibDbContext : DbContext
{
    public GamelibDbContext(DbContextOptions<GamelibDbContext> options) : base(options)
    {
    }

    public required DbSet<Game?> Games { get; set; }
    public required DbSet<Genre> Genres { get; set; }
    public required DbSet<Platform> Platforms { get; set; }
    public required DbSet<Setting> Settings { get; set; }
    public required DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .HasIndex(u => u.RawgId)
            .IsUnique();
    }
}