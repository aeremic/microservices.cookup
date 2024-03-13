using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Recipes.GetLikedRecipes;

public class GetLikedRecipeDto
{
    public long Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailPath { get; set; }

    public List<IngredientDto>? Ingredients { get; set; }
}