using MediatR;
using Users.Microservice.Common.Models;

namespace Users.Microservice.Queries.User.GetUserData;

public class GetUserDataQuery : IRequest<UserDataDto>
{
    public long Id { get; set; }
}