using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);

    Task<User?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken);
    
    Task<User?> GetIdByGuidAsync(Guid guid, CancellationToken cancellationToken);
}