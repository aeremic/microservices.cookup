namespace Users.Microservice.Common.Models.DTOs;

public class UserDataDto
{
    public long Id { get; set; }

    public string? Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Image { get; set; }
}