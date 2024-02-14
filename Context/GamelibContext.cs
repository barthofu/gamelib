using gamelib.Models;
using Microsoft.EntityFrameworkCore;

namespace gamelib.Context;

public class GamelibContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=gamelib.db");
        optionsBuilder.UseLazyLoadingProxies();
    }
}