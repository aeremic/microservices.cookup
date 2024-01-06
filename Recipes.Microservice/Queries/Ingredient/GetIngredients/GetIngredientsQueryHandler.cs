using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Infrastructure;

namespace Recipes.Microservice.Queries.Ingredient.GetIngredients;

public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, List<IngredientDto>>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public GetIngredientsQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<List<IngredientDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<IngredientDto>();
        try
        {
            var ingredients = await _repository.Ingredients.ToListAsync(cancellationToken);

            result = _mapper.Map<List<IngredientDto>>(ingredients);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}