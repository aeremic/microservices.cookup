using MediatR;
using Users.Microservice.Common.Models;
using Users.Microservice.Queries.User.GetUserData;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQuery : IRequest<List<UserDataDto>>
{
}