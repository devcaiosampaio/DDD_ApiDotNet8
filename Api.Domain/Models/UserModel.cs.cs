namespace Api.Domain.Models;
public class UserModel
{
    private Guid _id;

    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _name = string.Empty;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private string _email = string.Empty;

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    private DateTime _createAt;

    public DateTime CreateAt
    {
        get { return _createAt; }
        set { _createAt = value == null ? DateTime.UtcNow : value; }
    }
    private DateTime _updatedAt;

    public DateTime UpdatedAt
    {
        get { return _updatedAt; }
        set { _updatedAt = value; }
    }

}
