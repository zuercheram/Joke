using Joke.Server.Converter;
using Joke.Server.Models.Entities;
using Joke.Shared.Dtos;
using System.Text.Json;

namespace Joke.Server.Services;

public class JokeService
{
    private readonly HttpClient _httpClient;

    public JokeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JokeEntity> RequestRandomJokeAsync()
    {
        var httpResponse = await _httpClient.GetAsync("https://api.chucknorris.io/jokes/random");
        if (httpResponse == null)
        {
            throw new Exception("No random joke delivered!");
        }
        var options = new JsonSerializerOptions
        {
            Converters = { new CustomDateTimeConverter() }
        };
        var joke = JsonSerializer.Deserialize<JokeEntity>(await httpResponse.Content.ReadAsStringAsync(), options);
        if (joke == null)
        {
            throw new Exception("Joke deserialization failed!");
        }
        return joke;
    }

    public async Task<JokeDto> GetRandomJokeAsync()
    {
        var joke = await RequestRandomJokeAsync();
        return new JokeDto
        {
            SourceId = joke.SourceId,
            IconUrl = joke.IconUrl,
            Value = joke.Value,
            Url = joke.Url,
        };
    }
}
