using Joke.Server.Services;
using Joke.Shared.Dtos;
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

    [HttpGet("random")]
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAllJoke()
    {
        try
        {
            var jokes = await _jokeService.GetAllJokesAsync();
            return Ok(jokes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SaveJoke([FromBody] JokeDto joke)
    {
        try
        {
            await _jokeService.SaveJokeAsync(joke);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
