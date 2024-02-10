using AutoMapper;
using MediatR;
using NLog;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Domains.Interfaces;

namespace Users.Microservice.Queries.User.GetAllUsersData;

public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQuery, List<UserDataDto>>
{
    #region Properties

    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public GetUsersDataQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<List<UserDataDto>> Handle(GetUsersDataQuery request, CancellationToken cancellationToken)
    {
        var result = new List<UserDataDto>();
        try
        {
            var users = await _repository.Get(cancellationToken);

            result = _mapper.Map<List<UserDataDto>>(users);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}