using Microsoft.EntityFrameworkCore;
using Users.Microservice.Domain.Interfaces;
using Users.Microservice.Domain.Models;

namespace Users.Microservice.Infrastructure;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public Task<User?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Users
            .Where(user => user.Guid == guid)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Users
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}