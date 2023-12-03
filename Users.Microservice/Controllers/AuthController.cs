using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Microservice.Commands.Auth.ExternalLogin;

namespace Users.Microservice.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Properties

    private readonly IMediator _mediator;
    
    #endregion
    
    #region  Constructors

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Methods 

    /// <summary>
    /// Method for login in with a external provider.
    /// </summary>
    /// <param name="externalLoginCommand"></param>
    /// <returns>Data transfer object containing access token.</returns>
    [HttpPost]
    public async Task<ActionResult<ExternalLoginDto>> ExternalLogin([FromBody] ExternalLoginCommand? externalLoginCommand)
    {
        if (externalLoginCommand == null)
        {
            return BadRequest();
        }
        
        return await _mediator.Send(externalLoginCommand);
    }
    
    #endregion
}   