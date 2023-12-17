using AutoMapper;
using Recipes.Microservice.Domains;
using Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

namespace Recipes.Microservice.Mappers;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, GetRecommendedRecipeDto>();
    }
}