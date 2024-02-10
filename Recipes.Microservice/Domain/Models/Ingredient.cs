namespace Recipes.Microservice.Domain.Models;

public class Ingredient : Entity
{
    public string? Name { get; set; }

    public virtual ICollection<Recipe>? Recipes { get; set; }
}