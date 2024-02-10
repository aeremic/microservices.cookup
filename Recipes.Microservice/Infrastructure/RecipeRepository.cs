using Microsoft.EntityFrameworkCore;
using Recipes.Microservice.Domain.Interfaces;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Infrastructure;

public class RecipeRepository : Repository<Recipe>, IRecipeRepository
{
    public RecipeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<Recipe?> GetByIdWithIngredientsAsync(long id, CancellationToken cancellationToken)
    {
        return await ApplicationDbContext.Recipes
            .Where(recipe => recipe.Id == id)
            .Include(recipe => recipe.Ingredients)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Recipe>> GetRecipesContainingIngredients(List<long> ingredientIds,
        CancellationToken cancellationToken)
    {
        return await ApplicationDbContext.Recipes
            .Include(recipe => recipe.Ingredients)
            .Where(recipe =>
                recipe.Ingredients!.Any(ingredient => ingredientIds.Contains(ingredient.Id)))
            .ToListAsync(cancellationToken);
    }
}