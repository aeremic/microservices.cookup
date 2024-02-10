namespace Recipes.Microservice.Domain.Models;

public class Recipe : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Instructions { get; set; }
    public TimeSpan? TimeNeeded { get; set; }
    public int? Complexity { get; set; }
    public int? Calories { get; set; }
    public int? PlateQuantity { get; set; }
    public string? ThumbnailPath { get; set; }
    public DateTime? CreatedOn { get; set; }
    public long? CreatedBy { get; set; }
    public bool IsUserCreated { get; set; }
    
    public virtual ICollection<Ingredient>? Ingredients { get; set; }
}