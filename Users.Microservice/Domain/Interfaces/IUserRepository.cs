using Users.Microservice.Domain.Models;

namespace Users.Microservice.Domain.Interfaces;

public interface IUserRepository
{
    Task<List<User>> Get(CancellationToken cancellationToken);

    Task<User?> Get(long id, CancellationToken cancellationToken);

    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);

    Task AddAsync(User user, CancellationToken cancellationToken);
}