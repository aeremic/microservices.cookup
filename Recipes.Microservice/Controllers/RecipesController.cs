using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Microservice.Commands.Recipes.LikeRecipe;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Queries.Recipes.GetRecipe;
using Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

namespace Recipes.Microservice.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class RecipesController : ControllerBase
{
    #region Properties

    private readonly ISender _sender;

    #endregion

    #region Constructors

    public RecipesController(ISender sender)
    {
        _sender = sender;
    }

    #endregion

    #region Methods

    [HttpPost("[action]")]
    public async Task<ActionResult<List<GetRecommendedRecipeDto>>> GetRecommendedRecipes(
        [FromBody] GetRecommendedRecipesQuery request)
    {
        return await _sender.Send(request);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<GetRecipeDto>> GetRecipe([FromBody] GetRecipeQuery request)
    {
        return await _sender.Send(request);
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<bool>> LikeRecipe(
        [FromBody] LikeRecipeCommand likeRecipeCommand)
    {
        return await _sender.Send(likeRecipeCommand);
    }

    #endregion
}