namespace Api.Domain.Models;
public class UserModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    private DateTime _createAt;

    public DateTime CreateAt
    {
        get { return _createAt; }
        set { _createAt = value == default ? DateTime.UtcNow : value; }
    }

    public DateTime UpdatedAt { get; set; }

}
