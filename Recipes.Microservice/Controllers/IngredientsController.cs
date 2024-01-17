using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Queries.Ingredient.GetIngredients;

namespace Recipes.Microservice.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class IngredientsController : ControllerBase
{
    #region Properties

    private readonly ISender _sender;

    #endregion

    #region Constructors

    public IngredientsController(ISender sender)
    {
        _sender = sender;
    }

    #endregion

    #region Methods

    [HttpGet]
    public async Task<ActionResult<List<IngredientDto>>> GetIngredients()
    {
        return await _sender.Send(new GetIngredientsQuery());
    }

    #endregion
}