namespace Users.Microservice.Domains.Models;

public class User : Entity
{
    public Guid Guid { get; set; }
    public required string Email { get; set; }
    public required int Role { get; set; }
    public string? Username { get; set; }
    public string? ImageFullPath { get; set; }
}