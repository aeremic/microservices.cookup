using System.Text.Json;
using AutoMapper;
using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Common.Models.DTOs.Serializations;
using Recipes.Microservice.Domain.Interfaces;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, RecipeDto?>
{
    #region Properties

    private readonly IRecipeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetRecipeQueryHandler(IRecipeRepository repository, IMapper mapper, IFileService fileService,
        ILoggerService logger)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;

        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<RecipeDto?> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        RecipeDto? result = default;
        try
        {
            var recipe = await _repository.GetByIdWithIngredientsAsync(request.Id, cancellationToken);

            if (recipe != null)
            {
                result = _mapper.Map<RecipeDto>(recipe);
                result.Steps = !string.IsNullOrEmpty(recipe.Instructions)
                    ? JsonSerializer.Deserialize<List<StepDto>>(recipe.Instructions)
                    : null;

                if (!string.IsNullOrEmpty(result.ThumbnailPath))
                {
                    result.ThumbnailPath = _fileService.FormFileUrl(result.ThumbnailPath ?? string.Empty);
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