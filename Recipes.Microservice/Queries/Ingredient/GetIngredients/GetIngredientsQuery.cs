using MediatR;
using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Ingredient.GetIngredients;

public class GetIngredientsQuery : IRequest<List<IngredientDto>>
{
}