using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Microservice.Commands.Auth.ExternalLogin;

namespace Users.Microservice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    #region Properties

    private readonly ISender _sender;
    
    #endregion
    
    #region  Constructors

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    #endregion

    #region Methods 

    /// <summary>
    /// Method for login in with a external provider.
    /// </summary>
    /// <param name="externalLoginCommand"></param>
    /// <returns>Data transfer object containing access token.</returns>
    [HttpPost("[action]")]
    public async Task<ActionResult<ExternalLoginDto>> ExternalLogin([FromBody] ExternalLoginCommand? externalLoginCommand)
    {
        if (externalLoginCommand == null)
        {
            return BadRequest();
        }
        
        return await _sender.Send(externalLoginCommand);
    }
    
    #endregion
}   