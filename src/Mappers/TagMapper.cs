using System.Text.Json;
using gamelib.Models;

namespace gamelib.Mappers;

public class TagMapper
{
    public static Tag RawgTagToTag(JsonElement rawTag)
    {
        return new Tag
        {
            Name = rawTag.GetProperty("name").GetString() ?? "No Tag"
        };
    }
}