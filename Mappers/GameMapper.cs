using System.Text.Json;
using gamelib.Models;

namespace gamelib.Mappers;

public class GameMapper
{
    public static Game RawgGameToGame(JsonElement rawgGame)
    {
        return new Game
        {
            Title = rawgGame.GetProperty("name").GetString() ?? "No Title",
            ReleaseDate = rawgGame.GetProperty("released").GetString() ?? "No Release Date",
            CoverImage = rawgGame.GetProperty("background_image").GetString() ?? "No Image",
            Rating = rawgGame.GetProperty("rating").GetDouble(),
            RawgId = rawgGame.GetProperty("id").GetInt32(),
            Description = rawgGame.TryGetProperty("description", out var description)
                ? description.GetString() ?? "No Description"
                : "No Description",
            IsStarred = false,
            // relations
            Platforms = rawgGame.GetProperty("platforms")
                .EnumerateArray()
                .Select(item => PlatformMapper.RawgPlatformToPlatform(item.GetProperty("platform")))
                .ToArray(),
            Genres = rawgGame.GetProperty("genres")
                .EnumerateArray()
                .Select(GenreMapper.RawgGenreToGenre)
                .ToArray(),
            Tags = rawgGame.GetProperty("tags")
                .EnumerateArray()
                .Select(TagMapper.RawgTagToTag)
                .ToArray()
        };
    }
}