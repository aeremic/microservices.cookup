namespace Users.Microservice.Commands.Auth.ExternalLogin;

public class ExternalLoginDto
{
    public bool IsSuccess { get; set; } = false;
    public string? Token { get; set; }
    public string? Provider { get; set; }
    public bool IsNewUser { get; set; }
}