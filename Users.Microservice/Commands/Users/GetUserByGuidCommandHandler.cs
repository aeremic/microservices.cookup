using MediatR;
using Users.Microservice.Common.Interfaces;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Domain.Interfaces;

namespace Users.Microservice.Commands.Users;

public class GetUserByGuidCommandHandler : IRequestHandler<GetUserByGuidCommand, UserDataDto>
{
    #region Properties

    private readonly IUserRepository _repository;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetUserByGuidCommandHandler(IUserRepository repository, ILoggerService logger)
    {
        _repository = repository;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<UserDataDto> Handle(GetUserByGuidCommand request, CancellationToken cancellationToken)
    {
        var result = new UserDataDto();
        try
        {
            var userInDb =  await _repository.GetByGuidAsync(request.Guid, cancellationToken);
            if (userInDb != null)
            {
                result.Email = userInDb.Email;
                result.Username = userInDb.Username;
                result.ImageFullPath = userInDb.ImageFullPath;
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