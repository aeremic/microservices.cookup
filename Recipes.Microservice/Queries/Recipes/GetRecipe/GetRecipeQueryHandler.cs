using System.Text.Json;
using AutoMapper;
using MediatR;
using Recipes.Microservice.Common.Interfaces;
using Recipes.Microservice.Common.Models.DTOs;
using Recipes.Microservice.Common.Models.DTOs.Serializations;
using Recipes.Microservice.Domain.Interfaces;

namespace Recipes.Microservice.Queries.Recipes.GetRecipe;

public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, GetRecipeDto?>
{
    #region Properties

    private readonly IRecipeRepository _recipeRepository;
    private readonly IUserRecipesRepository _userRecipesRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ILoggerService _logger;

    #endregion

    #region Constructors

    public GetRecipeQueryHandler(IRecipeRepository recipeRepository, IUserRecipesRepository userRecipesRepository,
        IUserRepository userRepository, IMapper mapper, IFileService fileService,
        ILoggerService logger)
    {
        _recipeRepository = recipeRepository;
        _userRecipesRepository = userRecipesRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _fileService = fileService;

        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<GetRecipeDto?> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        GetRecipeDto? result = default;
        try
        {
            var recipeInDb = await _recipeRepository.GetByIdWithIngredientsAsync(request.RecipeId, cancellationToken);
            if (recipeInDb != null)
            {
                result = _mapper.Map<GetRecipeDto>(recipeInDb);
                result.Steps = !string.IsNullOrEmpty(recipeInDb.Instructions)
                    ? JsonSerializer.Deserialize<List<StepDto>>(recipeInDb.Instructions)
                    : null;

                if (!string.IsNullOrEmpty(result.ThumbnailPath))
                {
                    result.ThumbnailPath = _fileService.FormFileUrl(result.ThumbnailPath ?? string.Empty);
                }

                var userInDb = await _userRepository.GetByGuidAsync(request.UserGuid, cancellationToken);
                if (userInDb != null)
                {
                    result.IsRecipeLiked =
                        await _userRecipesRepository.DoesExistAsync(userInDb.Id, recipeInDb.Id,
                            cancellationToken);
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