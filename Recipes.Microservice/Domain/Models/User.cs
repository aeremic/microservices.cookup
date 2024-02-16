namespace Recipes.Microservice.Domain.Models;

public class User : Entity
{
    public Guid Guid { get; set; }
    public string? Username { get; set; }
    public string? ImageFullPath { get; set; }
}