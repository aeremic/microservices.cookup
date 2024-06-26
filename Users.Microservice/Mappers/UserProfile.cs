﻿using AutoMapper;
using Users.Microservice.Common.Models;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Domain.Models;

namespace Users.Microservice.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDataDto>();
    }
}