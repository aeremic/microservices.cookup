using AutoMapper;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Domain;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Mappers;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, IngredientDto>();
    }
}