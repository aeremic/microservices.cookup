namespace Users.Microservice.Domains;

public class User
{
    public long Id { get; set; }

    public required string Email { get; set; }
    public required int Role { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Image { get; set; }
}