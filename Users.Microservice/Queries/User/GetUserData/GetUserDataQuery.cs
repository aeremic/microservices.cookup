using MediatR;

namespace Users.Microservice.Queries.User.GetUserData;

public class GetUserDataQuery : IRequest<UserDataDto>
{
    public long Id { get; set; }
}