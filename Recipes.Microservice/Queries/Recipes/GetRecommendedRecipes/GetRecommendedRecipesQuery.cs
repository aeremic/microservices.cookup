using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

public class GetRecommendedRecipesQuery : IRequest<List<GetRecommendedRecipeDto>>
{
    [Required] public required List<long> PickedIngredients { get; set; }
}