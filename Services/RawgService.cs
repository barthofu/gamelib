using System.Net.Http;
using gamelib.Mappers;
using gamelib.Models;
using JsonDocument = System.Text.Json.JsonDocument;

namespace gamelib.Services;

public class RawgService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly int _pageSize = 10;

    public RawgService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.rawg.io/api/")
        };

        _apiKey = Environment.GetEnvironmentVariable("RAWG_API_KEY") ??
                  throw new Exception("RAWG_API_KEY not found in .env file.");
        
        Console.WriteLine(_apiKey);
    }

    public async Task<Game> GetGameAsync(int id)
    {
        var response = await _httpClient.GetAsync($"games/{id}?key={_apiKey}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var rawgGame = JsonDocument.Parse(content);
        
        return GameMapper.RawgGameToGame(rawgGame.RootElement);
    }

    public async Task<Game[]> SearchGamesAsync(string search)
    {
        var response = await _httpClient.GetAsync($"games?key={_apiKey}&search={search}&page_size={_pageSize}");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var rawgGames = JsonDocument.Parse(content);
        
        var games = rawgGames.RootElement.GetProperty("results")
            .EnumerateArray()
            .Select(GameMapper.RawgGameToGame)
            .ToArray();
        
        return games;
    }

}
