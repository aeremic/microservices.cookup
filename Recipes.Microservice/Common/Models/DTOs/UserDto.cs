namespace Recipes.Microservice.Common.Models.DTOs;

public class UserDto
{
    public Guid Guid { get; set; }
    public string? Username { get; set; }
    public string? ImageFullPath { get; set; }
}