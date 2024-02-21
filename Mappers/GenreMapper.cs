using System.Text.Json;
using gamelib.Models;

namespace gamelib.Mappers;

public class GenreMapper
{
    public static Genre RawgGenreToGenre(JsonElement rawGenre)
    {
        return new Genre
        {
            Name = rawGenre.GetProperty("name").GetString() ?? "No Genre"
        };
    }
}