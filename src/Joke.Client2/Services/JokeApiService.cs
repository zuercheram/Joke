using Joke.Shared.Dtos;
using System.Net.Http.Json;

namespace Joke.Client2.Services
{
    public class JokeApiService
    {
        private readonly HttpClient _httpClient;

        public JokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JokeDto?> GetRandomJoke()
        {
            return await _httpClient.GetFromJsonAsync<JokeDto>("api/joke/random");
        }

        // This Method sends a POST Request to the Backend along with the joke as JSON Object.
        public async Task SaveJoke(JokeDto joke)
        {
            await _httpClient.PostAsJsonAsync<JokeDto>("api/joke", joke);
        }

        public async Task<IList<JokeDto>> GetSavedJokes()
        {
            return await _httpClient.GetFromJsonAsync<IList<JokeDto>>("api/joke") ?? [];
        }
    }
}
