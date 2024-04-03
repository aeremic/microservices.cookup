namespace Recipes.Microservice.Domain.Models;

public class UserRecipe : Entity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long RecipeId { get; set; }
    public virtual Recipe Recipe { get; set; }
}