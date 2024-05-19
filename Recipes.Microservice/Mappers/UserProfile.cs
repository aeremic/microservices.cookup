using AutoMapper;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Domain.Models;

namespace Recipes.Microservice.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}