using MediatR;
using NLog;
using Users.Microservice.Common;
using Users.Microservice.Common.ExternalServices.GoogleGate;
using Users.Microservice.Domains;
using Users.Microservice.Infrastructure;
using Users.Microservice.Services;

namespace Users.Microservice.Commands.Auth.ExternalLogin;

public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, ExternalLoginDto>
{
    #region Properties

    private readonly IConfigurationSection _googleAuthConfigurationSection;
    private readonly Repository _repository;
    private readonly OAuthProxy _oAuthProxy;
    private readonly JwtHandler _jwtHandler;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public ExternalLoginCommandHandler(IConfiguration configuration, Repository repository, JwtHandler jwtHandler,
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
            var accessCodeDto = await OAuthProxy.ProcessGetAccessCodeAsync(
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.OAuth2Url).Value ??
                string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.TokenEndpoint)
                    .Value ?? string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.ClientId).Value ??
                string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.ClientSecret).Value ??
                string.Empty,
                _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.RedirectUri).Value ??
                string.Empty,
                request.AuthorizationCode);

            if (accessCodeDto == null || string.IsNullOrEmpty(accessCodeDto.AccessToken))
            {
                return result;
            }

            // Get userinfo by AccessToken

            // var settings = new GoogleJsonWebSignature.ValidationSettings()
            // {
            //     Audience = new List<string>()
            //     {
            //         _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.ClientId).Value ?? string.Empty
            //     }
            // };
            //
            // var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            // if (payload == null)
            // {
            //     return result;
            // }
            // result.Provider = request.Provider;
            //
            // var userInDb = await _repository.Users
            //     .Where(user => user.Email == payload.Email)
            //     .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            //
            // User? user;
            // if (userInDb != null)
            // {
            //     user = userInDb;
            // }
            // else
            // {
            //     user = new User
            //     {
            //         Guid = new Guid(),
            //         Email = payload.Email,
            //         Role = (int) Constants.Role.Regular
            //     };
            //     await _repository.AddAsync(user, cancellationToken);
            //     
            //     result.IsNewUser = true;
            // }
            //
            // var token = _jwtHandler.GenerateToken(user);
            //
            // if (!string.IsNullOrEmpty(token))
            // {
            //     result.Token = token;
            //     result.IsSuccess = true;
            // }
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}