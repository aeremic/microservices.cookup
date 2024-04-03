namespace Recipes.Microservice.Domain.Models;

public class Comment : Entity
{
    public string Content { get; set; }
    public int Rating { get; set; }
    
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long RecipeId { get; set; }
    public virtual Recipe Recipe { get; set; }
}