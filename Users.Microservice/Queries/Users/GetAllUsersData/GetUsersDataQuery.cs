using MediatR;
using Users.Microservice.Common.Models.DTOs;

namespace Users.Microservice.Queries.Users.GetAllUsersData;

public class GetUsersDataQuery : IRequest<List<UserDataDto>>
{
}