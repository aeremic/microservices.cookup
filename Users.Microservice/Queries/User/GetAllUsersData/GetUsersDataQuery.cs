using MediatR;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQuery : IRequest<List<UsersDataDto>>
{
}