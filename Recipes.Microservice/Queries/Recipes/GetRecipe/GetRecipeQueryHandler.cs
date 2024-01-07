using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using Recipes.Microservice.Common.Models;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Common.Models.DTOs.Serializations;
using Recipes.Microservice.Common.Services;
using Recipes.Microservice.Infrastructure;
using ILogger = NLog.ILogger;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, RecipeDto?>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;
    private readonly FileService _fileService;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public GetRecipeQueryHandler(Repository repository, IMapper mapper, FileService fileService)
    {
        _repository = repository;
        _mapper = mapper;
        _fileService = fileService;
        
        _fileService.Handler = new LocalFileServiceHandler();
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
                result.Steps = !string.IsNullOrEmpty(recipe.Instructions)
                    ? JsonSerializer.Deserialize<List<StepDto>>(recipe.Instructions)
                    : null;
                
                result.ThumbnailPath = _fileService.FormFileUrl(result.ThumbnailPath ?? string.Empty);
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