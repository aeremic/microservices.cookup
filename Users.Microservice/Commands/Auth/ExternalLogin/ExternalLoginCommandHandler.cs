using MediatR;
using NLog;
using Users.Microservice.Common;
using Users.Microservice.Common.ExternalServices.GoogleGate;
using Users.Microservice.Common.Services;
using Users.Microservice.Domain.Interfaces;
using Users.Microservice.Domain.Models;

namespace Users.Microservice.Commands.Auth.ExternalLogin;

public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, ExternalLoginDto>
{
    #region Properties

    private readonly IConfigurationSection _googleAuthConfigurationSection;
    private readonly IUserRepository _repository;
    private readonly OAuthProxy _oAuthProxy;
    private readonly JwtHandler _jwtHandler;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public ExternalLoginCommandHandler(IConfiguration configuration, IUserRepository repository, JwtHandler jwtHandler,
        OAuthProxy oAuthProxy)
    {
        _googleAuthConfigurationSection =
            configuration.GetSection(Constants.AuthConfigurationSectionKeys.AuthenticationGoogle);
        _repository = repository;
        _oAuthProxy = oAuthProxy;
        _jwtHandler = jwtHandler;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<ExternalLoginDto> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var result = new ExternalLoginDto();
        if (string.IsNullOrEmpty(request.AuthorizationCode))
        {
            return result;
        }

        try
        {
            var accessCodeDto = await _oAuthProxy.ProcessGetAccessCodeAsync(
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.AccountsBaseUrl)
                    .Value ?? string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.TokenEndpoint)
                    .Value ?? string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.ClientId)
                    .Value ?? string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.ClientSecret)
                    .Value ?? string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.RedirectUri)
                    .Value ?? string.Empty,
                request.AuthorizationCode);

            if (accessCodeDto == null || string.IsNullOrEmpty(accessCodeDto.AccessToken))
            {
                return result;
            }

            var userInfo = await _oAuthProxy.ProcessGetUserInfoAsync(_googleAuthConfigurationSection
                    .GetSection(Constants.AuthConfigurationSectionKeys.GoogleApisBaseUrl)
                    .Value ?? string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.UserInfoEndpoint)
                    .Value ?? string.Empty,
                accessCodeDto.AccessToken);

            if (userInfo?.Email == null)
            {
                return result;
            }

            var userInDb = await _repository.GetByEmail(userInfo.Email, cancellationToken);

            User? user;
            if (userInDb != null)
            {
                user = userInDb;
            }
            else
            {
                user = new User
                {
                    Guid = Guid.NewGuid(),
                    Email = userInfo.Email!,
                    Username = userInfo.Name,
                    ImageFullPath = userInfo.Picture!.ToString(),
                    Role = (int)Constants.Role.Regular
                };
                
                await _repository.AddAsync(user, cancellationToken);
                
                result.IsNewUser = true;
            }

            var token = _jwtHandler.GenerateToken(user);

            if (!string.IsNullOrEmpty(token))
            {
                result.Token = token;
                result.IsSuccess = true;
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