using MediatR;
using Users.Microservice.Common.Models.DTOs;

namespace Users.Microservice.Queries.Users.GetUserData;

public class GetUserDataQuery : IRequest<UserDataDto>
{
    public long Id { get; set; }
}