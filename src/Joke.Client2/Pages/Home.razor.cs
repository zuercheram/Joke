using Joke.Client2.Services;
using Joke.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace Joke.Client2.Pages
{
    public partial class Home : ComponentBase
    {
        private JokeDto? _joke;
        private IList<JokeDto> _jokeList;

        [Inject]
        private JokeApiService _jokeApiService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await RefreshRandomJokeAsync();
            await ReloadJokesAsync();
        }

        protected async Task SaveJokeAsync()
        {
            await _jokeApiService.SaveJoke(_joke);
            await RefreshRandomJokeAsync();
            await ReloadJokesAsync();
        }

        protected async Task RefreshRandomJokeAsync()
        {
            _joke = await _jokeApiService.GetRandomJoke();
        }

        private async Task ReloadJokesAsync()
        {
            _jokeList = await _jokeApiService.GetSavedJokes();
        }
    }
}
