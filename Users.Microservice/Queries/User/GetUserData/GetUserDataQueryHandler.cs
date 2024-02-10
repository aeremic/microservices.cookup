using AutoMapper;
using MediatR;
using NLog;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Domains.Interfaces;

namespace Users.Microservice.Queries.User.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, UserDataDto>
{
    #region Properties

    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public GetUserDataQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<UserDataDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var result = new UserDataDto();
        try
        {
            var user = await _repository.Get(request.Id, cancellationToken);

            if (user is not null)
            {
                result = _mapper.Map<UserDataDto>(user);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}