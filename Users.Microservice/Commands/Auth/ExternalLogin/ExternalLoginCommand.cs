using MediatR;

namespace Users.Microservice.Commands.Auth.ExternalLogin;

public class ExternalLoginCommand : IRequest<ExternalLoginDto>
{
    public string? AuthorizationCode { get; set; }
}