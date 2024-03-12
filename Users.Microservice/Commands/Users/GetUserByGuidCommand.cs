using MediatR;
using Users.Microservice.Common.Models.DTOs;

namespace Users.Microservice.Commands.Users;

public class GetUserByGuidCommand : IRequest<UserDataDto>
{
    public Guid Guid { get; set; }
}