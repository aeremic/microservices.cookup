using MediatR;

namespace Recipes.Microservice.Queries.Comments.GetComments;

public class GetCommentsQuery : IRequest<List<CommentDto>>
{
    public long RecipeId { get; set; }
}