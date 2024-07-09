
namespace Api.Domain.Models;

public class BaseModel
{

    public Guid Id { get; set; }

    private DateTime _createAt;
    public DateTime CreateAt
    {
        get { return _createAt; }
        set { _createAt = value == default ? DateTime.UtcNow : value; }
    }
    public DateTime UpdatedAt { get; set; }

}

