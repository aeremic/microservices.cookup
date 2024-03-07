using MediatR;
using Users.Microservice.Common.Models.DTOs;

namespace Users.Microservice.Commands.Users;

public class GetUserByEmailCommand : IRequest<UserDataDto>
{
    public string Email { get; set; }
}