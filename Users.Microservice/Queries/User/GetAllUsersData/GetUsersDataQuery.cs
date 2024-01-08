using MediatR;
using Users.Microservice.Common.Models;
using Users.Microservice.Common.Models.DTOs;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQuery : IRequest<List<UserDataDto>>
{
}