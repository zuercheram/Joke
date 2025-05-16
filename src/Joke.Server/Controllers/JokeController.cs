using Joke.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Joke.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JokeController : ControllerBase
{
    private readonly JokeService _jokeService;

    public JokeController(JokeService jokeService)
    {
        _jokeService = jokeService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetRandomJoke()
    {
        try
        {
            var randomJoke = await _jokeService.GetRandomJokeAsync();
            return Ok(randomJoke);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
