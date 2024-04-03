namespace Users.Microservice.Common.Models.DTOs;

public class UserDataDto
{
    public long Id { get; set; }
    
    public Guid Guid { get; set; }
    public string? Email { get; set; }
    public int Role { get; set; }
    public string? Username { get; set; }
    public string? ImageFullPath { get; set; }
}