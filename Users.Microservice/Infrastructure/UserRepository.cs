using Microsoft.EntityFrameworkCore;
using Users.Microservice.Domains.Interfaces;
using Users.Microservice.Domains.Models;

namespace Users.Microservice.Infrastructure;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Users
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}