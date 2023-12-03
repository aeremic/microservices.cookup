using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Microservice.Infrastructure;

namespace Users.Microservice.Queries.User.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, UserDataDto>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public GetUserDataQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<UserDataDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.Users.Where((user) => user.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<UserDataDto>(user);
    }

    #endregion
}