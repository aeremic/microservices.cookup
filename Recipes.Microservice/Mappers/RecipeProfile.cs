using System.Text.Json;
using AutoMapper;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Domains;
using Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

namespace Recipes.Microservice.Mappers;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, GetRecommendedRecipeDto>();
        CreateMap<Recipe, RecipeDto>();
    }
}

// configuration.MapFrom(
//     recipe => 
// )