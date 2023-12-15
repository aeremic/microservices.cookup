using MediatR;
using Users.Microservice.Common.Models;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQuery : IRequest<List<UserDataDto>>
{
}