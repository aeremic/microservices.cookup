using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Microservice.Infrastructure;
using Users.Microservice.Queries.User.GetUserData;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, List<UserDataDto>>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public GetUsersDataQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<List<UserDataDto>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.Users.ToListAsync(cancellationToken);

        return _mapper.Map<List<Users.Microservice.Models.User>, List<UserDataDto>>(users);
    }

    #endregion
}