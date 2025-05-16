using Joke.Client.Data;
using Joke.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace Joke.Client.Pages;

public partial class Home : ComponentBase
{
    private JokeDto? _joke;

    [Inject]
    private LocalJokeStore _jokeStore { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _joke = await _jokeStore.FetchLocalJokeAsync();
    }
}
