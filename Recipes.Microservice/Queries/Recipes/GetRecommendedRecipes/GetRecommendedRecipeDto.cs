
using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

public class GetRecommendedRecipeDto
{
    public required long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailPath { get; set; }

    public List<IngredientDto>? Ingredients { get; set; }
}