using gamelib.Context;
using gamelib.Models;
using Microsoft.EntityFrameworkCore;

namespace gamelib.Services;

public class GameService
{
    private readonly GamelibDbContext _dbContext;

    public GameService(GamelibDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> GameExists(Game game)
    {
        return GameExists(game.RawgId);
    }

    public Task<bool> GameExists(int rawgId)
    {
        // we use the rawg id to check if the game is already in the database
        return _dbContext.Games.AnyAsync(g => g.RawgId == rawgId);
    }

    public async Task<bool> AddGame(Game game)
    {
        if (await GameExists(game)) return false;
        await _dbContext.AddAsync(game);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public void RemoveGame(Game game)
    {
        _dbContext.Games.Remove(game);
        _dbContext.SaveChanges();
    }

    public Task<List<Game>> GetGames()
    {
        return _dbContext.Games.ToListAsync();
    }

    public Task<Game?> GetGame(int id)
    {
        return _dbContext.Games.FindAsync(id).AsTask();
    }

    public Task<List<Game>> SearchGames(string query)
    {
        return _dbContext.Games
            .Where(game => game.Title.ToLower().Contains(query.ToLower()))
            .ToListAsync();
    }
}