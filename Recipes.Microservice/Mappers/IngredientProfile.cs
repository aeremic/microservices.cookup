using AutoMapper;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Domains;

namespace Recipes.Microservice.Mappers;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, IngredientDto>();
    }
}