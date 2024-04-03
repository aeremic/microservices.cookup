using System.ComponentModel.DataAnnotations;
using MediatR;
using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQuery  : IRequest<GetRecipeDto>
{
    public long RecipeId { get; set; }
    public Guid UserGuid { get; set; }
}