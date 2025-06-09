namespace Joke.Shared.Dtos;

public class JokeDto
{
    public int Id { get; set; }
    public string SourceId { get; set; }
    public string Value { get; set; }
    public string Url { get; set; }
    public string IconUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static JokeDto ToDto(int id, string sourceId, string value, string url, string iconUrl, DateTime createAt, DateTime updateAt) => new JokeDto { Id = id, SourceId = sourceId, Value = value, Url = url, IconUrl = iconUrl, CreatedAt = createAt, UpdatedAt = updateAt };
}
