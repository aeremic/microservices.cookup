using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface IUserRecipesRepository
{
    Task<UserRecipe?> GetAsync(long userId, long recipeId, CancellationToken cancellationToken);

    Task AddAsync(UserRecipe userRecipe, CancellationToken cancellationToken);

    Task RemoveAsync(UserRecipe userRecipe, CancellationToken cancellationToken);

    Task<bool> DoesExistAsync(long userId, long recipeId, CancellationToken cancellationToken);
}