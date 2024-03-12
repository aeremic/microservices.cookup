using AutoMapper;
using MediatR;
using Users.Microservice.Common.Interfaces;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Domain.Interfaces;

namespace Users.Microservice.Queries.Users.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, UserDataDto>
{
    #region Properties

    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetUserDataQueryHandler(IUserRepository repository, IMapper mapper, ILoggerService logger)
    {
        _repository = repository;
        _mapper = mapper;
        
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<UserDataDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var result = new UserDataDto();
        try
        {
            var user = await _repository.GetAsync(request.Id, cancellationToken);

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