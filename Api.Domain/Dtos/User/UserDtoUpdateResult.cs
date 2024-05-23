namespace Api.Domain.Dtos.User;
public record struct UserDtoUpdateResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime UpdateAt { get; set; }
}
