using Users.Microservice.Domain.Models;

namespace Users.Microservice.Common.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
}