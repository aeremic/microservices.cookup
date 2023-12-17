namespace Recipes.Microservice.Domains;

public class Recipe
{
    public required long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Instructions { get; set; }
    public string? ThumbnailPath { get; set; }
    public DateTime? CreatedOn { get; set; }
    public long? CreatedBy { get; set; }
    public bool IsUserCreated { get; set; }
    
    public virtual ICollection<Ingredient>? Ingredients { get; set; }
}