using Joke.Shared.Dtos;
using System.Text.Json.Serialization;

namespace Joke.Server.Models.Entities;

public class JokeEntity
{
    public int Id { get; set; }
    [JsonPropertyName("id")]
    public string SourceId { get; set; }
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("icon_url")]
    public string IconUrl { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    public static JokeEntity ToEntity(JokeDto dto) => new() { Id = dto.Id, SourceId = dto.SourceId, Value = dto.Value, Url = dto.Url, IconUrl = dto.IconUrl, CreatedAt = dto.CreatedAt, UpdatedAt = dto.UpdatedAt };
}
