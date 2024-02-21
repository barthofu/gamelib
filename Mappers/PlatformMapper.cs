using System.Text.Json;
using gamelib.Models;

namespace gamelib.Mappers;

public class PlatformMapper
{
    public static Platform RawgPlatformToPlatform(JsonElement rawPlatform)
    {
        return new Platform
        {
            Name = rawPlatform.GetProperty("name").GetString() ?? "No Platform"
        };
    }
}