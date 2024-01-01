namespace Recipes.Microservice.Common.Models;

public class RecipeDto
{
    public required long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Instructions { get; set; }
    
    public virtual ICollection<IngredientDto>? Ingredients { get; set; }
}