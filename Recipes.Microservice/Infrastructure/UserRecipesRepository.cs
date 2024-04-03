using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class UserRecipesRepository : Repository<UserRecipe>, IUserRecipesRepository
{
    public UserRecipesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public Task<UserRecipe?> GetAsync(long userId, long recipeId, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.UserRecipes.Where(ur => ur.UserId == userId && ur.RecipeId == recipeId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> DoesExistAsync(long userId, long recipeId, CancellationToken cancellationToken)
    {
        return ApplicationDbContext.UserRecipes.Where(ur => ur.UserId == userId && ur.RecipeId == recipeId)
            .AnyAsync(cancellationToken);
    }
}