using MediatR;

namespace Recipes.Microservice.Queries.Recipes.GetLikedRecipes;

public class GetLikedRecipesQuery : IRequest<List<GetLikedRecipeDto>>
{
    public Guid UserGuid { get; set; }
}