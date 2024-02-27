using gamelib.Context;
using gamelib.Models;
using Microsoft.EntityFrameworkCore;

namespace gamelib.Services;

public class GameService(GamelibDbContext dbContext)
{
    public Task<bool> GameExists(Game game)
    {
        return GameExists(game.RawgId);
    }

    public Task<bool> GameExists(int rawgId)
    {
        // we use the rawg id to check if the game is already in the database
        return dbContext.Games.AnyAsync(g => g.RawgId == rawgId);
    }

    public async Task<bool> AddGame(Game game)
    {
        if (await GameExists(game)) return false;
        await dbContext.AddAsync(game);

        return await dbContext.SaveChangesAsync() > 0;
    }

    public void RemoveGame(Game game)
    {
        dbContext.Games.Remove(game);
        dbContext.SaveChanges();
    }

    public Task<List<Game>> GetGames()
    {
        return dbContext.Games.ToListAsync();
    }

    public Task<Game?> GetGame(int id)
    {
        return dbContext.Games
            .FindAsync(id)
            .AsTask();
    }

    public Task<List<Game>> SearchGames(string query)
    {
        return dbContext.Games
            .Where(game => game.Title.ToLower().Contains(query.ToLower()))
            .ToListAsync();
    }
    
    public Task<Game?> GetRandomGame()
    {
        return dbContext.Games
            .OrderBy(_ => EF.Functions.Random())
            .FirstOrDefaultAsync();
    }
}