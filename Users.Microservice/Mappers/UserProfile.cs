using AutoMapper;
using Users.Microservice.Common.Models;
using Users.Microservice.Domains;
using Users.Microservice.Queries.User.GetAllUsersData;
using Users.Microservice.Queries.User.GetUserData;

namespace Users.Microservice.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDataDto>();
    }
}