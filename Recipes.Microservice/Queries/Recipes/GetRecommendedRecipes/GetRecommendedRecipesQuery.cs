using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

public class GetRecommendedRecipesQuery : IRequest<List<GetRecommendedRecipeDto>>
{
    public List<long> PickedIngredients { get; set; }
}