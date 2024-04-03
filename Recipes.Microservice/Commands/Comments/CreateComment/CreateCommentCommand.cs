using MediatR;

namespace Recipes.Microservice.Commands.Comments.CreateComment;

public class CreateCommentCommand : IRequest<bool>
{
    public long RecipeId { get; set; }
    public Guid UserGuid { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}