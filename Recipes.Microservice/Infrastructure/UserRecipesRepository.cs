using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class UserRecipesRepository : Repository<UserRecipe>, IUserRecipesRepository
{
    public UserRecipesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}