using System.Text.Json.Serialization;

namespace Api.Domain.Dtos.User;
public record struct UserDtoUpdateResult
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("UpdateAt")]
    public DateTime UpdateAt { get; set; }
}
