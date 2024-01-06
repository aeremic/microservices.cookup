using System.ComponentModel.DataAnnotations;
using MediatR;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQuery  : IRequest<RecipeDto>
{
    [Required] public long Id { get; set; }
}