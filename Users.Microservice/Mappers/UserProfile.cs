using AutoMapper;
using Users.Microservice.Models;
using Users.Microservice.Queries.User.GetAllUsersData;
using Users.Microservice.Queries.User.GetUserData;

namespace Users.Microservice.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDataDto>();
        CreateMap<User, UsersDataDto>();
    }
}