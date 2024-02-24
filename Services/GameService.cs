using gamelib.Context;
using gamelib.Models;

namespace gamelib.Services;

public class GameService
{
    private readonly GamelibDbContext _dbContext;

    public GameService(GamelibDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool GameExists(Game game)
    {
        return GameExists(game.RawgId);
    }

    public bool GameExists(int rawgId)
    {
        // we use the rawg id to check if the game is already in the database
        return _dbContext.Games.Any(g => g.RawgId == rawgId);
    }

    public bool AddGame(Game game)
    {
        if (GameExists(game)) return false;
        _dbContext.Add(game);

        return _dbContext.SaveChanges() > 0;
    }

    public void RemoveGame(Game game)
    {
        _dbContext.Remove(game);
        _dbContext.SaveChanges();
    }

    public Game[] GetGames()
    {
        return _dbContext.Games.ToArray()!;
    }

    public Game? GetGame(int id)
    {
        return _dbContext.Games.Find(id);
    }

    public Game[] SearchGames(string query)
    {
        return _dbContext.Games
            .Where(game => game!.Title.ToLower().Contains(query.ToLower()))
            .ToArray()!;
    }
}