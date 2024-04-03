using AutoMapper;
using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Domain.Interfaces;

namespace Recipes.Microservice.Queries.Recipes.GetRecommendedRecipes;

public class
    GetRecommendedRecipesQueryHandler : IRequestHandler<GetRecommendedRecipesQuery, List<GetRecommendedRecipeDto>>
{
    #region Properties

    private readonly IRecipeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetRecommendedRecipesQueryHandler(IRecipeRepository repository, IMapper mapper, IFileService fileService,
        IConfiguration configuration, ILoggerService logger)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;

        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<List<GetRecommendedRecipeDto>> Handle(GetRecommendedRecipesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new List<GetRecommendedRecipeDto>();
        try
        {
            if (request.PickedIngredients.Any())
            {
                var recipes =
                    await _repository.GetRecipesContainingIngredients(request.PickedIngredients, cancellationToken);

                result = _mapper.Map<List<GetRecommendedRecipeDto>>(recipes);

                foreach (var item in result
                             .Where(item => !string.IsNullOrEmpty(item.ThumbnailPath)))
                {
                    item.ThumbnailPath = _fileService.FormFileUrl(item.ThumbnailPath ?? string.Empty);
                }
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