using System.ComponentModel.DataAnnotations;
using MediatR;
using Recipes.Microservice.Common.Models.DTOs;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQuery  : IRequest<RecipeDto>
{
    public long Id { get; set; }
}