using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Microservice.Domain.Models;

namespace Users.Microservice.Common.Services;

public class JwtHandler
{
    #region Properties

    private readonly IConfigurationSection _jwtConfigurationSection;

    #endregion

    #region Constructors

    public JwtHandler(IConfiguration configuration)
    {
        _jwtConfigurationSection = configuration.GetSection(Constants.JwtConfigurationSectionKeys.Jwt);
    }

    #endregion

    #region Methods

    public string GenerateToken(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfigurationSection.GetSection(
            Constants.JwtConfigurationSectionKeys.SecurityKey).Value ?? string.Empty);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtConfigurationSection[Constants.JwtConfigurationSectionKeys.ValidIssuer],
            audience: _jwtConfigurationSection[Constants.JwtConfigurationSectionKeys.ValidAudience],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_jwtConfigurationSection[Constants.JwtConfigurationSectionKeys.ExpiryInMinutes])),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }

    #endregion
}