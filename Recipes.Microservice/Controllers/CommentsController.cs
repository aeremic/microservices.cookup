using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Microservice.Commands.Comments.CreateComment;
using Recipes.Microservice.Queries.Comments.GetComments;

namespace Recipes.Microservice.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CommentsController
{
    #region Properties

    private readonly ISender _sender;

    #endregion

    #region Constructors

    public CommentsController(ISender sender)
    {
        _sender = sender;
    }

    #endregion

    #region Methods
    
    [HttpPost("[action]")]
    public async Task<ActionResult<List<CommentDto>>> GetComments(
        [FromBody] GetCommentsQuery getCommentsQuery)
    {
        return await _sender.Send(getCommentsQuery);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<bool>> CreateComment(
        [FromBody] CreateCommentCommand createCommentCommand)
    {
        return await _sender.Send(createCommentCommand);
    }
    
    #endregion
}