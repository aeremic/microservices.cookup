using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Comments.GetComments;

public class CommentDto
{
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public long UserId { get; set; }
    public virtual UserDto User { get; set; }
    public long RecipeId { get; set; }
    public virtual RecipeDto Recipe { get; set; }
}