using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Queries.Ingredient.GetIngredients;

namespace Recipes.Microservice.Controllers;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class IngredientsController : ControllerBase
{
    #region Properties

    private readonly IMediator _mediator;

    #endregion

    #region Constructors

    public IngredientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Methods

    [HttpGet]
    public async Task<ActionResult<List<IngredientDto>>> GetIngredients()
    {
        return await _mediator.Send(new GetIngredientsQuery());
    }

    #endregion
}