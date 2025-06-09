using Joke.Shared.Dtos;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace Joke.Client.Data;

public class LocalJokeStore
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _js;

    public LocalJokeStore(HttpClient httpClient, IJSRuntime js)
    {
        _httpClient = httpClient;
        _js = js;
    }

    public async Task<JokeDto?> FetchLocalJokeAsync()
    {
        await DeleteAllAsync();
        var newJoke = await _httpClient.GetFromJsonAsync<JokeDto>("api/joke/random");
        if (newJoke == null)
        {
            var jokes = await GetAllAsync<JokeDto[]>("latestjoke");
            return jokes.Length > 0 ? jokes[0] : null;
        }
        await PutAsync("latestjoke", null, newJoke);
        return newJoke;
    }

    public async Task DeleteAllAsync()
    {
        var localJokes = await GetAllAsync<JokeDto[]>("latestjoke");
        foreach (var item in localJokes)
        {
            await DeleteAsync("latestjoke", item.SourceId);
        }
    }

    ValueTask<T> GetAsync<T>(string storeName, object key)
        => _js.InvokeAsync<T>("localJokeStore.get", storeName, key);

    ValueTask<T> GetAllAsync<T>(string storeName)
        => _js.InvokeAsync<T>("localJokeStore.getAll", storeName);

    ValueTask PutAsync<T>(string storeName, object key, T value)
        => _js.InvokeVoidAsync("localJokeStore.put", storeName, key, value);

    ValueTask DeleteAsync(string storeName, object key)
        => _js.InvokeVoidAsync("localJokeStore.delete", storeName, key);
}
