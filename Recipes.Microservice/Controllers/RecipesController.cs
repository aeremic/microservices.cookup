using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

namespace Recipes.Microservice.Controllers;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class RecipesController : ControllerBase
{
    #region Properties

    private readonly IMediator _mediator;

    #endregion
    
    #region Constructors

    public RecipesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #endregion
    
    #region Methods

    [HttpPost("[action]")]
    public async Task<ActionResult<List<GetRecommendedRecipeDto>>> GetRecommendedRecipes(
        [FromBody] GetRecommendedRecipesQuery request)
    {
        return await _mediator.Send(request);
    }
    
    #endregion
}