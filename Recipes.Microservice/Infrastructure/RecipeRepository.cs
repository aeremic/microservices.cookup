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

    public async Task<List<Recipe>> GetRecipesWithUserRecipesByUserIdAsync(long userId,
        CancellationToken cancellationToken)
    {
        var query = from recipe in ApplicationDbContext.Recipes
            join userRecipe in ApplicationDbContext.UserRecipes on recipe.Id equals userRecipe.RecipeId
            where userRecipe.UserId == userId
            select recipe;

        return await query.ToListAsync(cancellationToken);
    }
}