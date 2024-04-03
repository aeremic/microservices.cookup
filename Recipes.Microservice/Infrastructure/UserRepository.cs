using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public Task<User?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.Users.Where(user => user.Guid == guid).FirstOrDefaultAsync(cancellationToken);
    }
}