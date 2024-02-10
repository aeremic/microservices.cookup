using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Domain.Interfaces;

public interface IRecipeRepository
{
    public Task<Recipe?> GetByIdWithIngredientsAsync(long id, CancellationToken cancellationToken);

    public Task<List<Recipe>> GetRecipesContainingIngredients(List<long> ingredientIds,
        CancellationToken cancellationToken);
}