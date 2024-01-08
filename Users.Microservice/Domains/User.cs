namespace Users.Microservice.Domains;

public class User
{
    public long Id { get; set; }
    public Guid Guid { get; set; }
    public required string Email { get; set; }
    public required int Role { get; set; }
    public string? Username { get; set; }
    public string? ImageFullPath { get; set; }
}