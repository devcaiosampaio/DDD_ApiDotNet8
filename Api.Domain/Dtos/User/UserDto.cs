using System.Text.Json.Serialization;

namespace Api.Domain.Dtos.User;

public record struct UserDto
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("CreateAt")]
    public DateTime CreateAt { get; set; }
}
