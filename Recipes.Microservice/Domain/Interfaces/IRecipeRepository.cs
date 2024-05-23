using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface IRecipeRepository
{
    public Task<Recipe?> GetByIdWithIngredientsAsync(long id, CancellationToken cancellationToken);

    public Task<List<Recipe>> GetRecipesContainingIngredients(List<long> ingredientIds,
        CancellationToken cancellationToken);

    public Task<List<Recipe>> GetRecipesWithUserRecipesByUserIdAsync(long userId, CancellationToken cancellationToken);
    
    public Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken);

    public Task<Recipe> GetAsync(long recipeId, CancellationToken cancellationToken);
}