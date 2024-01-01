using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using Recipes.Microservice.Infrastructure;
using ILogger = NLog.ILogger;

namespace Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

public class GetRecommendedRecipesQueryHandler : IRequestHandler<GetRecommendedRecipesQuery, List<GetRecommendedRecipeDto>>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    
    #endregion
    
    #region Constructors

    public GetRecommendedRecipesQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
    }
    
    #endregion
    
    #region Methods
    
    public async Task<List<GetRecommendedRecipeDto>> Handle(GetRecommendedRecipesQuery request, CancellationToken cancellationToken)
    {
        var result = new List<GetRecommendedRecipeDto>();
        try
        {
            if (request.PickedIngredients.Any())
            {
                var recipes = await _repository.Recipes
                    .Include(recipe => recipe.Ingredients)
                    .Where(recipe =>
                        recipe.Ingredients!.Any(ingredient => request.PickedIngredients.Contains(ingredient.Id)))
                    .ToListAsync(cancellationToken);

                result = _mapper.Map<List<GetRecommendedRecipeDto>>(recipes);
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