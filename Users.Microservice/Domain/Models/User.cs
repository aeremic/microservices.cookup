using System.ComponentModel.DataAnnotations;

namespace Users.Microservice.Domain.Models;

public class User : Entity
{
    public Guid Guid { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    public string? Username { get; set; }
    public string? ImageFullPath { get; set; }
}