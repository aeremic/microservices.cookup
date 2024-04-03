using MediatR;

namespace Recipes.Microservice.Commands.Recipes.LikeRecipe;

public class LikeRecipeCommand : IRequest<bool>
{
    public long RecipeId { get; set; }
    public Guid UserGuid { get; set; }
}