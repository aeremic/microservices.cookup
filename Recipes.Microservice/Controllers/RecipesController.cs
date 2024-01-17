using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Microservice.Common.Models;
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

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<RecipeDto>> GetRecipe(long id)
    {
        return await _sender.Send(new GetRecipeQuery { Id = id });
    }

    #endregion
}