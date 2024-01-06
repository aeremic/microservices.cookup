using Recipes.Microservice.Common.Models.DTOs.Serializations;

namespace Recipes.Microservice.Common.Models.DTOs;

public sealed class RecipeDto
{
    public required long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TimeSpan? TimeNeeded { get; set; }
    public int? Complexity { get; set; }
    public int? Calories { get; set; }
    public int? PlateQuantity { get; set; }
    
    public ICollection<StepDto>? Steps { get; set; }
    public ICollection<IngredientDto>? Ingredients { get; set; }
}