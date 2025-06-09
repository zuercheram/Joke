using Joke.Server.Converter;
using Joke.Server.Extensions;
using Joke.Server.Models.Entities;
using Joke.Server.Repositories;
using Joke.Shared.Dtos;
using System.Text.Json;

namespace Joke.Server.Services;

public class JokeService
{
    private readonly HttpClient _httpClient;
    private readonly JokeRepository _repository;

    public JokeService(HttpClient httpClient, JokeRepository jokeRepository)
    {
        _httpClient = httpClient;
        _repository = jokeRepository;
    }

    // Called by the controller to get and return a random joke
    public async Task<JokeDto> GetRandomJokeAsync()
    {
        var joke = await RequestRandomJokeAsync();
        return CreateJokeDto(joke);
    }

    // The next two methods are responsible for saving and retreiving Jokes to database
    public async Task<List<JokeDto>> GetAllJokesAsync()
    {
        var jokes = await _repository.GetJokesAsync();
        return [.. jokes.Select(CreateJokeDto)];
    }

    public async Task SaveJokeAsync(JokeDto joke)
    {
        var entity = JokeEntity.ToEntity(joke);

        await _repository.SaveJokeAsync(entity);
    }

    // From here there are only service local helper methods. Therefore, the methods scope is private.
    private JokeDto CreateJokeDto(JokeEntity joke)
    {
        return new JokeDto
        {
            SourceId = joke.SourceId,
            IconUrl = joke.IconUrl,
            Value = joke.Value,
            Url = joke.Url,
            CreatedAt = joke.CreatedAt,
            UpdatedAt = joke.UpdatedAt,
            Id = joke.Id
        };
    }

    // Gets a random joke from chucknorris.io
    private async Task<JokeEntity> RequestRandomJokeAsync()
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
}
