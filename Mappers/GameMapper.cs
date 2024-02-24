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
            RawgId = rawgGame.GetProperty("id").GetInt32(),
            ReleaseDate = rawgGame.TryGetProperty("released", out var released)
                ? released.GetString() ?? "No Release Date"
                : "No Release Date",
            CoverImage = rawgGame.TryGetProperty("background_image", out var coverImage)
                ? coverImage.GetString() ?? "No Image"
                : "No Image",
            Rating = rawgGame.TryGetProperty("rating", out var rating) ? rating.GetDouble() : 0.0,
            Description = rawgGame.TryGetProperty("description", out var description)
                ? description.GetString() ?? "No Description"
                : "No Description",
            IsStarred = false,
            // relations
            Platforms = rawgGame.TryGetProperty("platforms", out var platforms)
                ? platforms.EnumerateArray()
                    .Select(item => PlatformMapper.RawgPlatformToPlatform(item.GetProperty("platform")))
                    .ToArray()
                : Array.Empty<Platform>(),
            Genres = rawgGame.TryGetProperty("genres", out var genres)
                ? genres.EnumerateArray()
                    .Select(GenreMapper.RawgGenreToGenre)
                    .ToArray()
                : Array.Empty<Genre>(),
            Tags = rawgGame.TryGetProperty("tags", out var tags)
                ? tags.EnumerateArray()
                    .Select(TagMapper.RawgTagToTag)
                    .ToArray()
                : Array.Empty<Tag>()
        };
    }
}