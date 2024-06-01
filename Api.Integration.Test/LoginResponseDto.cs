namespace Api.Integration.Test;
public class LoginResponseDto
{
    public bool authenticated { get; set; }
    public DateTime create { get; set; }
    public DateTime expiration { get; set; }
    public string accessToken { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string message { get; set; } = string.Empty;
}
