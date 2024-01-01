using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Infrastructure;
using ILogger = NLog.ILogger;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, RecipeDto?>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public GetRecipeQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<RecipeDto?> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        RecipeDto? result = default;
        try
        {
            var recipe = await _repository.Recipes
                .Where(recipe => recipe.Id == request.Id)
                .Include(recipe => recipe.Ingredients)
                .FirstOrDefaultAsync(cancellationToken);

            if (recipe != null)
            {
                result = _mapper.Map<RecipeDto>(recipe);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}