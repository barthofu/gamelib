using Microsoft.EntityFrameworkCore;

namespace gamelib.Context;

public class GamelibContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=gamelib.db");
        optionsBuilder.UseLazyLoadingProxies();
    }
}