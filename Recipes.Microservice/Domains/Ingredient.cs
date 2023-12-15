namespace Recipes.Microservice.Domains;

public class Ingredient
{
    public required long Id { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<Recipe>? Recipes { get; set; }
}