using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Microservice.Commands.Users;
using Users.Microservice.Common.Models.DTOs;
using Users.Microservice.Queries.Users.GetAllUsersData;
using Users.Microservice.Queries.Users.GetUserData;

namespace Users.Microservice.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    #region Properties

    private readonly ISender _sender;

    #endregion

    #region Constructors

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Method for retrieving user data. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User data.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDataDto>> GetUserData(long id)
    {
        return await _sender.Send(new GetUserDataQuery { Id = id });
    }

    /// <summary>
    /// Method for retrieving users data.
    /// </summary>
    /// <returns>Users data as a list.</returns>
    [HttpGet]
    public async Task<ActionResult<List<UserDataDto>>> GetUsersData()
    {
        return await _sender.Send(new GetUsersDataQuery());
    }

    /// <summary>
    /// Method for retrieving users data by email.
    /// </summary>
    /// <param name="getUserByGuidCommand"></param>
    /// <returns>User data.</returns>
    [HttpPost("[action]")]
    public async Task<ActionResult<UserDataDto>> GetUserByGuid([FromBody] GetUserByGuidCommand? getUserByGuidCommand)
    {
        if (getUserByGuidCommand == null)
        {
            return BadRequest();
        }

        return await _sender.Send(getUserByGuidCommand);
    }

    #endregion
}