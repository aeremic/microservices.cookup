using Google.Apis.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Microservice.Common;
using Users.Microservice.Domains;
using Users.Microservice.Infrastructure;
using Users.Microservice.Services;

namespace Users.Microservice.Commands.Auth.ExternalLogin;

public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, ExternalLoginDto>
{
    #region Properties

    private readonly IConfigurationSection _googleAuthConfigurationSection;
    private readonly Repository _repository;
    private readonly JwtHandler _jwtHandler;

    #endregion

    #region Constructors

    public ExternalLoginCommandHandler(IConfiguration configuration, Repository repository, JwtHandler jwtHandler)
    {
        _googleAuthConfigurationSection = configuration.GetSection(Constants.AuthConfigurationSectionKeys.AuthenticationGoogle);
        _repository = repository;
        _jwtHandler = jwtHandler;
    }

    #endregion

    #region Methods

    public async Task<ExternalLoginDto> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var result = new ExternalLoginDto();
        if(string.IsNullOrEmpty(request.IdToken) || string.IsNullOrEmpty(request.Email))
        {
            return result;
        }

        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                {
                    _googleAuthConfigurationSection.GetSection(Constants.AuthConfigurationSectionKeys.ClientId).Value ?? string.Empty
                }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            if (payload == null)
            {
                return result;
            }
            result.Provider = request.Provider;

            var userInDb = await _repository.Users
                .Where(user => user.Email == payload.Email)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            User? user;
            if (userInDb != null)
            {
                user = userInDb;
            }
            else
            {
                user = new User
                {
                    Email = payload.Email,
                    Role = (int) Constants.Role.Regular
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
        catch
        {
            // TODO: Log exception
        }

        return result;
    }

    #endregion
}